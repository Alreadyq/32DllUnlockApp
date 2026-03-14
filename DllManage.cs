using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace _32DllUnlockApp
{
    
    class DllManager
    {
        static Dictionary<string, IntPtr> cache = new Dictionary<string, IntPtr>();

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string path);

        public static IntPtr GetDll(string path)
        {
            if (cache.ContainsKey(path))
                return cache[path];

            IntPtr dll = LoadLibrary(path);

            if (dll == IntPtr.Zero)
                throw new Exception("LoadLibrary失败");

            cache[path] = dll;

            return dll;
        }
    }
}
