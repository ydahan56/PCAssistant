using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sdk
{
    public static class PCManager
    {
        public static string Combine(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public static string CombineExternal(Assembly assembly, string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(assembly.Location), fileName);
        }
    }
}
