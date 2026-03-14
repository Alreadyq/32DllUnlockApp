using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace _32DllUnlockApp
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //Logger.Log("Service Start");

            //Task.Run(() =>
            //{
            //    HttpServer.Start();
            //});

            //Console.WriteLine();

            if (!Environment.UserInteractive)
            {
                RunService(args);
                return;
            }

            // 如果没有参数，自动安装+启动
            if (args.Length == 0)
            {
                string exe = Process.GetCurrentProcess().MainModule.FileName;


                if (!ServiceController
                    .GetServices()
                    .Any(s => s.ServiceName == "SeedKeyService"))
                {
                    Process.Start(exe, "install");
                    Process.Start(exe, "start");
                }

                return;

            }

            RunService(args);


        }




        static void RunService(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<SeedKeyService>(s =>
                {
                    s.ConstructUsing(name => new SeedKeyService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();


                x.SetServiceName("UdsSeedKeyService");
                x.SetDisplayName("UdsSeedKeyService");
                x.SetDescription("BTS-UDS SeedKey HTTP Service");

                x.StartAutomatically();
            });
        }
    }



}
