using capapp.Helpers;
using CommandLine;
using Sdk.Models;
using Sdk.Plugins;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

// Requires Windows SDK Projections:
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Foundation;
using WinRT;

namespace capapp
{
    [Verb("/capapp", HelpText = "Capture the window of a program using Windows Graphics Capture")]
    public class DllMain : Plugin
    {
        [Option("pid", Required = true, HelpText = "The Process Id of the program to capture")]
        public int ProcessId { get; set; }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        // --- COM Interop for Windows Graphics Capture ---

        [ComImport]
        [Guid("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IGraphicsCaptureItemInterop
        {
            IntPtr CreateForWindow([In] IntPtr window, [In] ref Guid iid);
            IntPtr CreateForMonitor([In] IntPtr monitor, [In] ref Guid iid);
        }

        [DllImport("combase.dll")]
        private static extern int RoGetActivationFactory(
            [MarshalAs(UnmanagedType.HString)] string activatableClassId,
            ref Guid iid,
            out IntPtr factory);

        // --- COM Interop for Direct3D 11 ---

        [DllImport("d3d11.dll", ExactSpelling = true, PreserveSig = true)]
        private static extern int D3D11CreateDevice(
            IntPtr pAdapter,
            int driverType,
            IntPtr software,
            uint flags,
            IntPtr pFeatureLevels,
            uint featureLevels,
            uint sdkVersion,
            out IntPtr ppDevice,
            out int pFeatureLevel,
            out IntPtr ppImmediateContext);

        [DllImport("d3d11.dll", ExactSpelling = true, PreserveSig = true)]
        private static extern int CreateDirect3D11DeviceFromDXGIDevice(
            IntPtr dxgiDevice,
            out IntPtr graphicsDevice);


        public override void Execute()
        {
            try
            {
                if (ProcessId <= 0)
                {
                    SendResult("Invalid process ID.", false);
                    return;
                }

                IntPtr hwnd = FindMainWindow((uint)ProcessId);

                if (hwnd == IntPtr.Zero)
                {
                    SendResult($"No visible window was found for process {ProcessId}.", false);
                    return;
                }

                byte[] jpeg = CaptureWindowWGC(hwnd);

                if (jpeg == null || jpeg.Length == 0)
                {
                    SendResult("Failed to capture the window using Graphics Capture.", false);
                    return;
                }

                var stream = new MemoryStream(jpeg, writable: false);
                var fileName = $"capture_{ProcessId}_{DateTime.UtcNow.Ticks}.jpg";

                this.ExecuteContextCallback(new ImageContext()
                {
                    IsErrorSuccess = true,
                    Stream = stream,
                    FileName = fileName,
                    ChatId = this.Parameters.ChatId,
                    ReplyParameters = this.Parameters.ReplyParameters
                });
            }
            catch (Exception e)
            {
                SendResult(e.Message, false);
            }
        }

        private static IntPtr FindMainWindow(uint pid)
        {
            IntPtr result = IntPtr.Zero;

            EnumWindows((hWnd, _) =>
            {
                GetWindowThreadProcessId(hWnd, out uint windowPid);

                if (windowPid != pid || !IsWindowVisible(hWnd))
                    return true;

                result = hWnd;
                return false;
            }, IntPtr.Zero);

            return result;
        }

        private byte[] CaptureWindowWGC(IntPtr hwnd)
        {
            // 1. Create the WinRT GraphicsCaptureItem
            GraphicsCaptureItem item = CreateItemForWindow(hwnd);

            if (item == null)
            {
                SendResult($"Window 0x{hwnd:X8} is not eligible for capture (e.g., Chrome, protected content, or unsupported rendering).", false);
                return null;
            }

            // 2. Create the WinRT Direct3D Device
            IDirect3DDevice device = CreateDirect3DDevice();

            // 3. Setup the Frame Pool and Session
            using (var pool = Direct3D11CaptureFramePool.CreateFreeThreaded(
                device,
                DirectXPixelFormat.B8G8R8A8UIntNormalized,
                1,
                item.Size))
            using (var session = pool.CreateCaptureSession(item))
            {
                // Disable the yellow capture border if supported by OS (Windows 10 2004+)
                try { session.IsBorderRequired = false; } catch { /* Ignore on older OS */ }

                using (var mres = new ManualResetEventSlim(false))
                {
                    Direct3D11CaptureFrame currentFrame = null;

                    // 4. Hook frame arrival and start capture
                    pool.FrameArrived += (s, args) =>
                    {
                        if (currentFrame == null)
                        {
                            currentFrame = pool.TryGetNextFrame();
                            mres.Set();
                        }
                    };

                    session.StartCapture();

                    if (!mres.Wait(3000)) // 3-second timeout
                    {
                        throw new TimeoutException("Timed out waiting for a frame. The window might be minimized or purely off-screen.");
                    }

                    // 5. Encode the captured GPU surface to JPEG
                    using (currentFrame)
                    {
                        return EncodeSurfaceToJpeg(currentFrame.Surface);
                    }
                }
            }
        }

        private byte[] EncodeSurfaceToJpeg(IDirect3DSurface surface)
        {
            // Extract the surface into a CPU-readable SoftwareBitmap
            var softwareBitmapOp = SoftwareBitmap.CreateCopyFromSurfaceAsync(surface);
            var softwareBitmap = AwaitWinRT(softwareBitmapOp);

            using (softwareBitmap)
            using (var memoryStream = new InMemoryRandomAccessStream())
            {
                // Encode to JPEG using built-in WinRT codecs
                var encoderOp = BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, memoryStream);
                var encoder = AwaitWinRT(encoderOp);

                encoder.SetSoftwareBitmap(softwareBitmap);
                AwaitWinRT(encoder.FlushAsync());

                // Read bytes from the WinRT memory stream
                var size = (uint)memoryStream.Size;
                var reader = new DataReader(memoryStream.GetInputStreamAt(0));

                AwaitWinRT(reader.LoadAsync(size));

                byte[] bytes = new byte[size];
                reader.ReadBytes(bytes);

                return bytes;
            }
        }

        // --- Helper Methods to bridge Win32/COM -> WinRT ---

        [DllImport("combase.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true)]
        private static extern int WindowsCreateString(string sourceString, int length, out IntPtr hstring);

        [DllImport("combase.dll", ExactSpelling = true, PreserveSig = true)]
        private static extern int WindowsDeleteString(IntPtr hstring);

        [DllImport("combase.dll", ExactSpelling = true, PreserveSig = true)]
        private static extern int RoGetActivationFactory(IntPtr activatableClassId, ref Guid iid, out IntPtr factory);

        private static GraphicsCaptureItem CreateItemForWindow(IntPtr hwnd)
        {
            string className = "Windows.Graphics.Capture.GraphicsCaptureItem";
            WindowsCreateString(className, className.Length, out IntPtr hString);

            try
            {
                var interopGuid = new Guid("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356"); // IGraphicsCaptureItemInterop

                // Pass the raw HSTRING pointer instead of a managed string
                int hr = RoGetActivationFactory(hString, ref interopGuid, out IntPtr factoryPtr);
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                try
                {
                    // IGraphicsCaptureItemInterop is a plain IUnknown-based COM interface (not
                    // IInspectable/WinRT), so classic Marshal.GetObjectForIUnknown works fine here.
                    var interop = (IGraphicsCaptureItemInterop)Marshal.GetObjectForIUnknown(factoryPtr);
                    var itemGuid = new Guid("79C3F95B-31F7-4EC2-A464-632EF5D30760"); // IGraphicsCaptureItem

                    IntPtr itemPtr = interop.CreateForWindow(hwnd, ref itemGuid);

                    // CRITICAL: Check if CreateForWindow failed (returns IntPtr.Zero for incompatible windows)
                    if (itemPtr == IntPtr.Zero)
                    {
                        return null;
                    }

                    try
                    {
                        // GraphicsCaptureItem is a WinRT (IInspectable-based) runtime class.
                        // It must be wrapped via CsWinRT's MarshalInterface<T>.FromAbi, NOT via
                        // Marshal.GetObjectForIUnknown + cast, which throws "failed to create a
                        // CCW ... the specified cast is not valid" for WinRT ABI pointers.
                        GraphicsCaptureItem item = MarshalInterface<GraphicsCaptureItem>.FromAbi(itemPtr);
                        return item;
                    }
                    finally
                    {
                        Marshal.Release(itemPtr);
                    }
                }
                finally
                {
                    Marshal.Release(factoryPtr);
                }
            }
            finally
            {
                // Always clean up the HSTRING memory to prevent leaks
                if (hString != IntPtr.Zero)
                {
                    WindowsDeleteString(hString);
                }
            }
        }

        private static IDirect3DDevice CreateDirect3DDevice()
        {
            uint D3D11_CREATE_DEVICE_BGRA_SUPPORT = 0x20;
            int D3D_DRIVER_TYPE_HARDWARE = 1;
            uint D3D11_SDK_VERSION = 7;

            int hr = D3D11CreateDevice(
                IntPtr.Zero, D3D_DRIVER_TYPE_HARDWARE, IntPtr.Zero,
                D3D11_CREATE_DEVICE_BGRA_SUPPORT, IntPtr.Zero, 0,
                D3D11_SDK_VERSION, out IntPtr d3dDevice, out _, out IntPtr context);

            if (hr < 0) Marshal.ThrowExceptionForHR(hr);

            Guid dxgiDeviceGuid = new Guid("54ec77fa-1377-44e6-8c32-88fd5f44c84c"); // IDXGIDevice
            hr = Marshal.QueryInterface(d3dDevice, ref dxgiDeviceGuid, out IntPtr dxgiDevice);
            if (hr < 0) Marshal.ThrowExceptionForHR(hr);

            hr = CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice, out IntPtr winrtDevicePtr);
            if (hr < 0) Marshal.ThrowExceptionForHR(hr);

            IDirect3DDevice winrtDevice;

            // IDirect3DDevice is a WinRT (IInspectable-based) interface. It must be wrapped via
            // CsWinRT's MarshalInterface<T>.FromAbi, NOT Marshal.GetObjectForIUnknown + cast,
            // which throws "failed to create a CCW ... the specified cast is not valid".
            winrtDevice = MarshalInterface<IDirect3DDevice>.FromAbi(winrtDevicePtr);

            Marshal.Release(winrtDevicePtr);
            Marshal.Release(dxgiDevice);
            Marshal.Release(d3dDevice);
            if (context != IntPtr.Zero) Marshal.Release(context);

            return winrtDevice;
        }

        // Lightweight blocking helper for WinRT Async Operations (avoids Task context switches)
        private static T AwaitWinRT<T>(IAsyncOperation<T> operation)
        {
            using (var mres = new ManualResetEventSlim(false))
            {
                operation.Completed = (op, status) => mres.Set();
                mres.Wait();
                return operation.GetResults();
            }
        }

        private static void AwaitWinRT(IAsyncAction action)
        {
            using (var mres = new ManualResetEventSlim(false))
            {
                action.Completed = (act, status) => mres.Set();
                mres.Wait();
                action.GetResults();
            }
        }

        private void SendResult(string message, bool success)
        {
            this.ExecuteContextCallback(new TextContext()
            {
                ErrorMessage = message,
                IsErrorSuccess = success,
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });
        }
    }
}