using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Components
{
    public interface IImageComponent
    {
        public List<string> GetMarsRoverImagesFromFile(string fileName);
    }
}
