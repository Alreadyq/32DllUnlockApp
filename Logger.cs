using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DllUnlockApp
{
    class Logger
    {
        static string path =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            "logs", "service.log");

        public static void Log(string msg)
        {
            Directory.CreateDirectory("logs");

            File.AppendAllText(path,
                DateTime.Now + " " + msg + "\n");
        }
    }
}
