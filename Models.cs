using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _32DllUnlockApp
{
    class GenerateKeyRequest
    {
        public string symbol { get; set; }

        public string Proto_type { get; set; }

        public int level { get; set; }

        public byte[] seeds { get; set; }
    }

    class GenerateKeyResponse
    {
        [JsonProperty("key")]
        public byte[] key;

    }
}
