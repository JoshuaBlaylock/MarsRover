using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace MarsRover.Helpers
{
    public interface IClientHelper
    {
        public string GetStringResult(string path);
        public Stream GetStreamResult(string path);
    }
}
