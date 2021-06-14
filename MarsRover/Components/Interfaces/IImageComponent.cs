using System.Collections.Generic;

namespace MarsRover.Components
{
    public interface IImageComponent
    {
        public List<string> GetMarsRoverImagesFromFile(string fileName);
    }
}
