using System.Diagnostics;
using System.Text;

namespace apps.Helpers
{
    internal class BackgroundHelper
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            var items = Process.GetProcesses()
                .Where(x => x.SessionId != 0);

            foreach (Process item in items)
            {
                try
                {
                    var versionInfo = item.MainModule?.FileVersionInfo;
                    sb.AppendLine($"- {versionInfo?.FileDescription} ({item.Id})");
                }
                catch
                {
                    // ignore
                }
            }

            return sb.ToString();
        }
    }
}
