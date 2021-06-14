using MarsRover.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace MarsRover.Components
{
    public class ImageComponent : IImageComponent
    {
        HttpClient _client;
        const string ROVER_URL = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=";
        const string API_SUFFIX = "&api_key=qJ8ApUtWoBtT0QFnppleOsdyRwD0oqGzV50f1e73";
        const string STORAGE_LOCATION = @"C:\MarsRover\";
        public ImageComponent(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
        }

        public List<string> GetMarsRoverImagesFromFile(string fileName)
        {
            var result = new List<string>();
            var dates = System.IO.File.ReadAllLines($"{STORAGE_LOCATION}{fileName}").ToList();
            foreach (var date in dates)
            {
                if (DateTime.TryParse(date, out var validDate))
                {
                    var request = string.Format("{0}{1:yyyy-MM-dd}{2}", ROVER_URL, validDate, API_SUFFIX);
                    using (var response = _client.GetAsync(request).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        var model = JsonConvert.DeserializeObject<RoverResponseModel>(response.Content.ReadAsStringAsync().Result);
                        SaveImagesToDisk(validDate, model.Photos);
                        result.AddRange(model.Photos.Select(p => p.Img_src).ToList());
                    }
                }
            }
            return result;
        }

        private void SaveImagesToDisk(DateTime date, List<ImageModel> images)
        {
            var path = Path.Combine(STORAGE_LOCATION, string.Format("{0:yyyyMMdd}", date));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (var image in images)
            {
                var imagePath = Path.Combine(path, $"{image.Id}.jpg");
                if (File.Exists(imagePath))
                {
                    continue;
                }

                using (var response = _client.GetAsync(image.Img_src).Result)
                {
                    response.EnsureSuccessStatusCode();
                    using (var stream = response.Content.ReadAsStreamAsync().Result)
                    {
                        using (var fs = File.Create(imagePath))
                        {
                            var buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                            fs.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }
    }
}
