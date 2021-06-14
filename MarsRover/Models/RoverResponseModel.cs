using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
