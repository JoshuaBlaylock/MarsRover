using System.Collections.Generic;
using System.IO;

namespace MarsRover.Helpers
{
    public interface IFileHelper
    {
        public void FindOrCreateDirectory(string path);
        public bool FileExists(string path);
        public FileStream CreateFile(string path);
        public List<string> ReadLines(string path);
        public void ReadStreamToFile(Stream source, FileStream target);
    }
}
