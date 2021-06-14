using MarsRover.Helpers;
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
        IClientHelper _client;
        IFileHelper _file;
        const string ROVER_URL = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=";
        const string API_SUFFIX = "&api_key=qJ8ApUtWoBtT0QFnppleOsdyRwD0oqGzV50f1e73";
        const string STORAGE_LOCATION = @"C:\MarsRover\";
        public ImageComponent(IClientHelper client, IFileHelper file)
        {
            _client = client;
            _file = file;
        }

        public List<string> GetMarsRoverImagesFromFile(string fileName)
        {
            var result = new List<string>();
            var dates = _file.ReadLines($"{STORAGE_LOCATION}{fileName}");
            foreach (var date in dates)
            {
                if (DateTime.TryParse(date, out var validDate))
                {
                    var request = string.Format("{0}{1:yyyy-MM-dd}{2}", ROVER_URL, validDate, API_SUFFIX);
                    var response = _client.GetStringResult(request);
                    var model = JsonConvert.DeserializeObject<RoverResponseModel>(response);
                    SaveImagesToDisk(validDate, model.Photos);
                    result.AddRange(model.Photos.Select(p => p.Img_src).ToList());
                }
            }
            return result;
        }

        private void SaveImagesToDisk(DateTime date, List<ImageModel> images)
        {
            var path = Path.Combine(STORAGE_LOCATION, string.Format("{0:yyyyMMdd}", date));
            _file.FindOrCreateDirectory(path);
            foreach (var image in images)
            {
                var imagePath = Path.Combine(path, $"{image.Id}.jpg");
                if (_file.FileExists(imagePath))
                {
                    continue;
                }

                using (var stream = _client.GetStreamResult(image.Img_src))
                {
                    using (var fs = _file.CreateFile(imagePath))
                    {
                        _file.ReadStreamToFile(stream, fs);
                    }
                }
            }
        }
    }
}
