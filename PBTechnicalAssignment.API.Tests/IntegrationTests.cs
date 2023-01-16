using Newtonsoft.Json;
using PBTechnicalAssignment.API.Dtos;
using System.Net.Http.Json;
using System.Text;

namespace PBTechnicalAssignment.API.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private static int _testCount = 0;
        private ApiFixture _api;

        [TestInitialize]
        public void Init()
        {
            _api = new ApiFixture($"test{++_testCount}");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _api.Dispose();
        }

        [TestMethod]
        public async Task OrderPost_Succeeds()
        {
            var client = _api.CreateClient();

            var jsonData = JsonConvert.SerializeObject(new
            {
                Items = new[]
                {
                    new { ProductType = "photoBook", Quantity = 1 },
                    new { ProductType = "canvas", Quantity = 2 }
                }
            });
            var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/orders", contentData);

            var result = await response.Content.ReadFromJsonAsync<string>();
            Assert.IsTrue(response?.StatusCode == System.Net.HttpStatusCode.Created);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task OrderGet_WithInvalidId_Fails()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync("/orders/invalid_id");

            Assert.IsTrue(response?.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task OrderGet_WithValidId_Succeeds()
        {
            var client = _api.CreateClient();

            var orderId = _api.CreatedTestOrderId;

            var response = await client.GetAsync($"/orders/{orderId}");

            var result = await response.Content.ReadFromJsonAsync<OrderDto>();
            Assert.IsTrue(response?.IsSuccessStatusCode);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == orderId.ToString());
        }
    }
}