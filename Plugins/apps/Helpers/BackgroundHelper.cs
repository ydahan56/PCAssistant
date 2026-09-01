using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace apps.Helpers
{
    internal class BackgroundHelper
    {
        private const nint INVALID_HANDLE_VALUE = -1;

        [Flags]
        private enum SnapshotFlags : uint
        {
            TH32CS_SNAPPROCESS = 0x00000002
        }

        [Flags]
        private enum ProcessAccess : uint
        {
            QueryLimitedInformation = 0x1000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public nint th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern nint CreateToolhelp32Snapshot(
            SnapshotFlags dwFlags,
            uint th32ProcessID);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool Process32First(
            nint hSnapshot,
            ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool Process32Next(
            nint hSnapshot,
            ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ProcessIdToSessionId(
            uint dwProcessId,
            out uint pSessionId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern nint OpenProcess(
            ProcessAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(nint hObject);


        public override string ToString()
        {
            var sb = new StringBuilder();

            nint snapshot = CreateToolhelp32Snapshot(
                SnapshotFlags.TH32CS_SNAPPROCESS,
                0);

            if (snapshot == INVALID_HANDLE_VALUE)
                return sb.ToString();

            try
            {
                var entry = new PROCESSENTRY32
                {
                    dwSize = (uint)Marshal.SizeOf<PROCESSENTRY32>()
                };

                if (!Process32First(snapshot, ref entry))
                    return sb.ToString();

                do
                {
                    // Equivalent to filtering SessionId != 0.
                    if (!ProcessIdToSessionId(entry.th32ProcessID, out uint sessionId) ||
                        sessionId == 0)
                    {
                        continue;
                    }

                    nint process = OpenProcess(
                        ProcessAccess.QueryLimitedInformation,
                        false,
                        entry.th32ProcessID);

                    if (process == 0)
                        continue;

                    try
                    {
                        string? path = GetProcessPath(process);

                        if (path == null)
                            continue;

                        string? description = GetFileDescription(path);

                        sb.AppendLine(
                            $"- {description ?? entry.szExeFile} ({entry.th32ProcessID})");
                    }
                    catch
                    {
                        // Access denied / process exited / etc.
                    }
                    finally
                    {
                        CloseHandle(process);
                    }

                } while (Process32Next(snapshot, ref entry));
            }
            finally
            {
                CloseHandle(snapshot);
            }

            return sb.ToString();
        }

        private string? GetProcessPath(nint process)
        {
            Span<char> buffer = stackalloc char[1024];
            uint length = (uint)buffer.Length;

            if (!QueryFullProcessImageName(
                    process,
                    0,
                    buffer,
                    ref length))
            {
                return null;
            }

            return buffer[..(int)length].ToString();
        }

        private static string? GetFileDescription(string path)
        {
            uint ignored;

            uint size = GetFileVersionInfoSize(
                path,
                out ignored);

            if (size == 0)
                return null;

            byte[] buffer = new byte[size];

            if (!GetFileVersionInfo(
                    path,
                    0,
                    size,
                    buffer))
            {
                return null;
            }

            if (!VerQueryValue(
                    buffer,
                    @"\StringFileInfo\040904B0\FileDescription",
                    out nint value,
                    out uint length))
            {
                return null;
            }

            if (value == 0 || length == 0)
                return null;

            return Marshal.PtrToStringUni(value);
        }

        [DllImport("version.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern uint GetFileVersionInfoSize(
            string lptstrFilename,
            out uint lpdwHandle);

        [DllImport("version.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetFileVersionInfo(
            string lptstrFilename,
            uint dwHandle,
            uint dwLen,
            [Out] byte[] lpData);

        [DllImport("version.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VerQueryValue(
            byte[] pBlock,
            string lpSubBlock,
            out nint lplpBuffer,
            out uint puLen);


        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool QueryFullProcessImageName(
            nint hProcess,
            uint dwFlags,
            Span<char> lpExeName,
            ref uint lpdwSize);

    }
}
