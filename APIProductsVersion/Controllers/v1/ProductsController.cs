using APIProductsVersion.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIProductsVersion.Controllers.v1
{
    [ApiVersion("1.0")]
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

        [MapToApiVersion("1.0")]
        [HttpGet(Name = "GetProductsData")]
        public async Task<IActionResult> GetProductsDataAsync()
        {
            var response = await _httpClient.GetAsync(APITestURL);
            var content = await response.Content.ReadAsStringAsync();
            var productsData = JsonSerializer.Deserialize<List<Product>>(content);

            return Ok(productsData);

        }

    }
}
