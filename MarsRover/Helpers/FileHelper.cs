using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarsRover.Helpers
{
    public class FileHelper : IFileHelper
    {
        public FileStream CreateFile(string path)
        {
            return File.Create(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void FindOrCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public List<string> ReadLines(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        public void ReadStreamToFile(Stream source, FileStream target)
        {
            var buffer = new byte[source.Length];
            source.Read(buffer, 0, buffer.Length);
            target.Write(buffer, 0, buffer.Length);
        }
    }
}
