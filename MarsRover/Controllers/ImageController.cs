using MarsRover.Components;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MarsRover.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        IImageComponent _component;

        public ImageController(IImageComponent component)
        {
            _component = component;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _component.GetMarsRoverImagesFromFile("dates.txt");
        }
    }
}
