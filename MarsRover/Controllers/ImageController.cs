using MarsRover.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet("{id}")]
        public string Get(int id)
        {
            var response = (new HttpClient()).GetAsync("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2015-6-3&api_key=qJ8ApUtWoBtT0QFnppleOsdyRwD0oqGzV50f1e73").Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
