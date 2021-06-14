using MarsRover.Components;
using MarsRover.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    public class ImageComponentTests
    {
        Mock<IClientHelper> _client;
        Mock<IFileHelper> _file;
        ImageComponent _component;

        [SetUp]
        public void Setup()
        {
            _client = new Mock<IClientHelper>();
            _client.Setup(c => c.GetStringResult(It.IsAny<string>())).Returns(ImageComponentTestModels.imageModel);
            _client.Setup(c => c.GetStringResult("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2018-04-30&api_key=qJ8ApUtWoBtT0QFnppleOsdyRwD0oqGzV50f1e73")).Returns(ImageComponentTestModels.invalidImageModelString);
            _file = new Mock<IFileHelper>();
            _component = new ImageComponent(_client.Object, _file.Object);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 3)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        public void GetMarsRoverImagesFromFile(int idx, int count)
        {
            _file.Setup(f => f.ReadLines(It.IsAny<string>())).Returns(ImageComponentTestModels.Dates[idx]);
            var result = _component.GetMarsRoverImagesFromFile("Test");
            Assert.AreEqual(result.Count, count);
        }
    }

    public static class ImageComponentTestModels
    {
        public static List<List<string>> Dates = new List<List<string>>()
        {
            new List<string>(),
            new List<string>()
            {
                "02/27/17",
                "June 2, 2018",
                "Jul-13-2016",
                "April 31, 2018"
            },
            new List<string>()
            {
                "02/31/17"
            },
            new List<string>()
            {
                "April 30, 2018"
            }
        };

        public static string imageModel = "{\"photos\":[{\"id\":102685,\"sol\":1004,\"camera\":{\"id\":20,\"name\":\"FHAZ\",\"rover_id\":5,\"full_name\":\"Front Hazard Avoidance Camera\"},\"img_src\":\"http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FLB_486615455EDR_F0481570FHAZ00323M_.JPG\",\"earth_date\":\"2015-06-03\",\"rover\":{\"id\":5,\"name\":\"Curiosity\",\"landing_date\":\"2012-08-06\",\"launch_date\":\"2011-11-26\",\"status\":\"active\"}}]}";

        public static string invalidImageModelString = "{\"photos\":[]}";
    }
}