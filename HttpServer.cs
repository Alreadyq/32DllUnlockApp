using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace _32DllUnlockApp
{
   
    class HttpServer
    {
        public static void Start()
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add("http://localhost:59864/");

            listener.Start();

            Logger.Log("HTTP Server Started 59864");

            while (true)
            {
                var ctx = listener.GetContext();

                Task.Run(() => Handle(ctx));
            }
        }

        static void Handle(HttpListenerContext ctx)
        {
            try
            {
                 var reader =
                    new StreamReader(ctx.Request.InputStream);

                string body = reader.ReadToEnd();

                var req =JsonConvert.DeserializeObject<GenerateKeyRequest>(body);

                byte[] key = SeedKeyEngine.Generate(req);

                var resp = new GenerateKeyResponse
                {
                    key = key
                };

                WriteJson(ctx, resp);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());

               // WriteJson(ctx, new { message = ex.Message });
            }
        }

        static void WriteJson(HttpListenerContext ctx, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);

            byte[] buf = System.Text.Encoding.UTF8.GetBytes(json);

            ctx.Response.ContentType = "application/json";

            ctx.Response.OutputStream.Write(buf, 0, buf.Length);

            ctx.Response.Close();
        }
    }
}
