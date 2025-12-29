using MicroShop.Web.Models;
using MicroShop.Web.Services.Contracts;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MicroShop.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "https://localhost:7186/api/Categories/";

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        IEnumerable<CategoryViewModel> categoryVMs;

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categoryVMs = await JsonSerializer
                          .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
                return categoryVMs;
            }
            else
            {
                return null;
            }
        }
    }
}
