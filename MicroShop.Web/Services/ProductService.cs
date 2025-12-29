using MicroShop.Web.Models;
using MicroShop.Web.Services.Contracts;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MicroShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "https://localhost:7186/api/Products/";
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productVMs;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        PutToken(token, client);

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVMs = JsonSerializer.Deserialize<IEnumerable<ProductViewModel>>(apiResponse, _options);
                return productVMs;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<ProductViewModel> GetProductById(int productId, string token)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        PutToken(token, client);

        using (var response = await client.GetAsync(apiEndpoint + productId))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = JsonSerializer.Deserialize<ProductViewModel>(apiResponse, _options);
                return productVM;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        PutToken(token, client);

        StringContent content = new StringContent(
                                            JsonSerializer.Serialize(productVM), 
                                            Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer
                           .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                return productVM;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        PutToken(token, client);

        ProductViewModel updatedProduct = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                updatedProduct = await JsonSerializer
                           .DeserializeAsync<ProductViewModel>(apiResponse, _options);
                return updatedProduct;
            }
            else
            {
                return null;
            }
        }
    }

    public async Task<bool> DeleteProduct(int productId, string token)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        PutToken(token, client);

        using (var response = await client.DeleteAsync(apiEndpoint + productId))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private static void PutToken(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
    }
}
