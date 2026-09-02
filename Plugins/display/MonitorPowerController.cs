using System.Runtime.InteropServices;

namespace display
{
    public static class MonitorPowerController
    {
        // VCP Code for Display Power Mode
        private const byte VCP_POWER_MODE = 0xD6;

        // VCP Power States
        private const uint POWER_ON = 0x01;
        private const uint POWER_OFF = 0x04; // 0x04 is DPM: Standby/Off. 0x05 is physical hard off (rarely supported)

        public static bool SetPowerState(bool turnOn)
        {
            bool overallSuccess = false;
            uint targetPowerState = turnOn ? POWER_ON : POWER_OFF;

            // 1. Enumerate all logical monitors
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData) =>
            {
                uint physicalMonitorCount = 0;

                // 2. Get the number of physical monitors tied to this logical handle
                if (GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out physicalMonitorCount) && physicalMonitorCount > 0)
                {
                    var physicalMonitors = new PHYSICAL_MONITOR[physicalMonitorCount];

                    // 3. Get the physical monitor handles
                    if (GetPhysicalMonitorsFromHMONITOR(hMonitor, physicalMonitorCount, physicalMonitors))
                    {
                        foreach (var monitor in physicalMonitors)
                        {
                            // 4. Send the hardware command over DDC/CI
                            if (SetVCPFeature(monitor.hPhysicalMonitor, VCP_POWER_MODE, targetPowerState))
                            {
                                overallSuccess = true;
                            }
                        }
                        // 5. Clean up handles
                        DestroyPhysicalMonitors(physicalMonitorCount, physicalMonitors);
                    }
                }
                return true; // Continue enumeration
            }, IntPtr.Zero);

            return overallSuccess;
        }

        #region P/Invoke Definitions

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left, top, right, bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PHYSICAL_MONITOR
        {
            public IntPtr hPhysicalMonitor;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szPhysicalMonitorDescription;
        }

        private delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport("dxva2.dll", SetLastError = true)]
        private static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, out uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll", SetLastError = true)]
        private static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", SetLastError = true)]
        private static extern bool SetVCPFeature(IntPtr hMonitor, byte bVCPCode, uint dwNewValue);

        [DllImport("dxva2.dll", SetLastError = true)]
        private static extern bool DestroyPhysicalMonitors(uint dwPhysicalMonitorArraySize, [In] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        #endregion
    }
}