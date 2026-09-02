using CommandLine;
using Sdk.Models;
using Sdk.Plugins;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace kill
{
    [Verb("/kill", HelpText = "Kill a Process by its ID")]
    public class DllMain : Plugin
    {
        [Option("pid", Required = true, HelpText = "The Process ID, in decimal")]
        public int PID { get; set; }

        private const uint PROCESS_TERMINATE = 0x0001;
        private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
            uint dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TerminateProcess(
            IntPtr hProcess,
            uint uExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool QueryFullProcessImageName(
            IntPtr hProcess,
            uint dwFlags,
            [Out] char[] lpExeName,
            ref uint lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        private static string GetProcessName(IntPtr processHandle)
        {
            const int bufferSize = 1024;

            var buffer = new char[bufferSize];
            uint size = (uint)buffer.Length;

            if (!QueryFullProcessImageName(
                    processHandle,
                    0,
                    buffer,
                    ref size))
            {
                return "unknown";
            }

            string path = new string(buffer, 0, (int)size);

            return System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public override void Execute()
        {
            if (PID <= 0)
            {
                SendResult("PID must be a positive integer.", false);
                return;
            }

            IntPtr processHandle = OpenProcess(
                PROCESS_TERMINATE | PROCESS_QUERY_LIMITED_INFORMATION,
                false,
                (uint)PID);

            if (processHandle == IntPtr.Zero)
            {
                SendResult(
                    new Win32Exception(Marshal.GetLastWin32Error()).Message,
                    false);

                return;
            }

            try
            {
                string processName = GetProcessName(processHandle);

                if (!TerminateProcess(processHandle, 1))
                {
                    SendResult(
                        new Win32Exception(Marshal.GetLastWin32Error()).Message,
                        false);

                    return;
                }

                SendResult(
                    $"Process with name {processName} terminated",
                    true);
            }
            finally
            {
                CloseHandle(processHandle);
            }
        }

        private void SendResult(string text, bool success)
        {
            this.ExecuteContextCallback(new TextContext()
            {
                ErrorMessage = text,
                IsErrorSuccess = success,
                ChatId = this.Parameters.ChatId,
                ReplyParameters = this.Parameters.ReplyParameters
            });
        }
    }
}
