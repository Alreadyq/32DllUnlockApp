using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;


namespace _32DllUnlockApp
{
    
    class SeedKeyEngine
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string name);


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate int GenerateKeyExDelegate(
                    byte[]iSeedArray, uint iSeedArrayLen, uint iSecurityLevel,string iVarint,byte[]ioKeyArray,uint iKeyArraySize, ref uint oKeyArrayLen);

        public static byte[] Generate(GenerateKeyRequest req)
        {
            string dllName = Path.GetFileName(req.symbol);

            string dllPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Lib",
                dllName+".dll");

            IntPtr dll = DllManager.GetDll(dllPath);

            IntPtr func = GetProcAddress(dll, "GenerateKeyEx");

            if (func == IntPtr.Zero)
                throw new Exception("找不到GenerateKeyEx");

            GenerateKeyExDelegate calc =(GenerateKeyExDelegate) Marshal.GetDelegateForFunctionPointer(func,typeof(GenerateKeyExDelegate));

            byte[] seed =req.seeds;

            byte[] key = new byte[32];

            uint Osize = 0;
            int len = calc(seed, (uint)seed.Length, (uint)req.level,null,key,(uint)key.Length,ref Osize);

            string result = BytesToHex(key, len);

            Logger.Log($"DLL:{dllName} seed:{req.seeds} key:{result}");

            return key;
        }

        static byte[] HexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return bytes;
        }

        static string BytesToHex(byte[] data, int len)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
                sb.Append(data[i].ToString("X2"));

            return sb.ToString();
        }
    }
}
