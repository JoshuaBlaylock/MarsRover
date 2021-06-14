using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace MarsRover.Helpers
{
    public class ClientHelper : IClientHelper
    {
        HttpClient _client;
        public ClientHelper(IHttpClientFactory factory, IFileHelper file)
        {
            _client = factory.CreateClient();
        }

        public string GetStringResult(string path)
        {
            return _client.GetAsync(path).Result.Content.ReadAsStringAsync().Result;
        }

        public Stream GetStreamResult(string path)
        {
            return _client.GetAsync(path).Result.Content.ReadAsStreamAsync().Result;
        }
    }
}
