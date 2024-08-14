using System.Reflection;

namespace Sdk
{
    public static class PCManager
    {
        public static string Combine(string fileName)
        {
            return Path.Combine(PCManager.GetAppDirectory(), fileName);
        }

        public static string GetAppDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string CombineAssembly(Assembly assembly, string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(assembly.Location), fileName);
        }
    }
}
