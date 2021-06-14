using System;
using System.Collections.Generic;

namespace MarsRover.Models
{
    public class RoverResponseModel
    {
        public RoverResponseModel()
        {
            Photos = new List<ImageModel>();
        }

        public List<ImageModel> Photos { get; set; }
    }
}
