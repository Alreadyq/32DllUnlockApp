using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace _32DllUnlockApp
{
   
    class SeedKeyService
    {
        public bool Start()
        {
            Logger.Log("Service Start");

            Task.Run(() =>
            {
                HttpServer.Start();
            });

            return true;
        }

        public bool Stop()
        {
            Logger.Log("Service Stop");
            return true;
        }
    }
}
