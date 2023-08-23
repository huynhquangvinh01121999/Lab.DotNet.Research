using Api.Services;
using Api.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Test
{
    [TestClass]
    public class WeatherForecastUT
    {
        private HttpClient _httpClient;
        private const string ApiBaseUrl = "https://jsonplaceholder.typicode.com";
        private IWeatherForecastService _weatherForecastService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //public WeatherForecastUT()
        //{
        //    _weatherForecastService = new WeatherForecastService();
        //}

        // initial test
        [TestInitialize]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ApiBaseUrl);
            _weatherForecastService = new WeatherForecastService();
        }

        [TestMethod]
        [DataRow(0, DisplayName = "GetWeatherForeCast Testing")]
        public async Task TestGetWeatherForeCast(int counter)
        {
            var results = await _weatherForecastService.WeatherForecasts(Summaries);

            // case 1: data có null ko?
            Assert.IsNotNull(results, "WeatherForecasts is null");

            // case 2: số lượng record có > 0 ko?
            Assert.IsTrue(results.Count > counter, "WeatherForecasts is empty");

            // case 3: field Date not null
            foreach (var item in results)
            {
                Assert.IsNotNull(item.Date, "A Date is null or empty");
                Assert.IsNotNull(item.Summary, "A Summary is null");
                Assert.AreNotEqual("", item.Summary, "A Summary is empty");
            }
        }

        [TestMethod]
        public async Task TestApiEndpoint()
        {
            // Gửi HTTP request đến API endpoint
            HttpResponseMessage response = await _httpClient.GetAsync("/todos");

            // Kiểm tra xem request có thành công không (ví dụ: kiểm tra StatusCode)
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Đọc nội dung response và kiểm tra nó với giá trị mong đợi
            string responseContent = await response.Content.ReadAsStringAsync();

            // Chuyển đổi responseContent thành một đối tượng C# (ví dụ: JObject)
            var responseData = JsonConvert.DeserializeObject<List<Todo>>(responseContent);

            // Kiểm tra kiểu dữ liệu của responseData
            Assert.IsInstanceOfType(responseData, typeof(List<Todo>));

            foreach(var item in responseData)
            {
                // Kiểm tra kiểu dữ liệu của một trường cụ thể trong đối tượng responseData
                Assert.IsInstanceOfType(item.userId, typeof(int));
                Assert.IsInstanceOfType(item.id, typeof(int));
                Assert.IsInstanceOfType(item.title, typeof(string));
                Assert.IsInstanceOfType(item.completed, typeof(bool));

                // So sánh giá trị của trường cụ thể với giá trị mong đợi
                Assert.IsTrue(item.id > 0, "Id must be > 0");
            }
        }

        // clean up test
        [TestCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
