using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public static string CombineExternal(Assembly assembly, string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(assembly.Location), fileName);
        }
    }
}
