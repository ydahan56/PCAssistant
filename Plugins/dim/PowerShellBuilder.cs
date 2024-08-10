using System.Diagnostics;

namespace dim
{
    public enum PSWindowStyle
    {
        Normal,
        Minimized,
        Maximized,
        Hidden
    }

    public class PowerShellBuilder
    {
        private readonly List<string> argsList;

        public PowerShellBuilder()
        {
            this.argsList = new List<string>();
        }

        public PowerShellBuilder BypassExecutionPolicy()
        {
            this.argsList.Add("-ExecutionPolicy Bypass");
            return this;
        }

        public PowerShellBuilder SetWindowStyle(PSWindowStyle windowStyle)
        {
            this.argsList.Add($"-WindowStyle {Enum.GetName(windowStyle)}");
            return this;
        }

        public PowerShellBuilder SetFileScriptPath(string path)
        {
            this.argsList.Add($"-File \"{path}\"");
            return this;
        }

        public PowerShellBuilder SetArgument(object argument)
        {
            this.argsList.Add($"\"{argument}\"");
            return this;
        }

        public string ExecuteScript()
        {
            var scriptArguments = string.Join(" ", this.argsList);
            var processStartInfo = new ProcessStartInfo("powershell.exe", scriptArguments)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            return output;
        }
    }
}
