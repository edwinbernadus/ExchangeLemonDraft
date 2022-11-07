using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlueLight.Main
{
    public class DebugApiController : Controller
    {
        private readonly RpcBackEnd rpcBackEnd;

        public DebugApiController(RpcBackEnd rpcBackEnd)
        {
            this.rpcBackEnd = rpcBackEnd;
        }


        [Route("/api/debugApi")]
        //[HttpGet]
        [HttpPost]
        public async Task<string> Post()
        {
            var content = this.Request.Body;
            var sr = new StreamReader(content);
            var input = await sr.ReadToEndAsync();

            var output = $"{input} 123 456 789";
            return output;
        }


        // /api/debugApi

        [Route("/api/debugApi")]
        [HttpGet]
        public async Task<RpcOutput> Get()
        {
            try
            {
                var w = await rpcBackEnd.TestDebug();
                return w;
            }
            catch (Exception e)
            {
                var output = e.StackTrace.ToString();
                var output2 = new RpcOutput()
                {
                    Content = output
                };
                return output2;
            }
        }

        // /api/debugApi/hello
        [Route("/api/debugApi/hello")]
        [HttpGet]
        public async Task<string> GetTest2()
        {
            return "123";
        }
        
        // /api/debugApi/contentDebug
        [Route("/api/debugApi/contentDebug")]
        [HttpGet]
        public async Task<int> GetTest3()
        {
            var hostName = "http://localhost:5000";
            var client = new HttpClient();
            var s = await client.GetStringAsync(hostName);
            var total = s.Length;
            return total;
        }
        
        
    }
}