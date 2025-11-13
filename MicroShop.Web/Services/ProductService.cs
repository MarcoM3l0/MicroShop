using MicroShop.Web.Models;
using MicroShop.Web.Services.Contracts;
using System.Text.Json;

namespace MicroShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/products/";
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

    public Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> GetProductById(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }

}
