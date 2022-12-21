using APIProductsVersion.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIProductsVersion.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private const string APITestURL = "https://fakestoreapi.com/products";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetProductsData")]
        public async Task<IActionResult> GetProductsDataAsync()
        {
            var response = await _httpClient.GetAsync(APITestURL);
            var content = await response.Content.ReadAsStringAsync();
            var productsData = JsonSerializer.Deserialize<List<Productv2>>(content);

            return Ok(productsData);

        }

    }
}
