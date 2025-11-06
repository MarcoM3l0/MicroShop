using AutoMapper;
using MicroShop.ProductApi.DTOs;
using MicroShop.ProductApi.Models;
using MicroShop.ProductApi.Repositories;
using MicroShop.ProductApi.Services.interfaces;

namespace MicroShop.ProductApi.Services.concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsEntities = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(productsEntities);
    }

    public async Task<ProductDTO?> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO?>(productEntity);
    }

    public async Task AddProduct(ProductDTO productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Create(productEntity);
        productDto.ProductId = productEntity.ProductId;
    }
    

    public async Task UpdateProduct(ProductDTO productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Update(productEntity.ProductId, productEntity);
    }

    public async Task DeleteProduct(int id)
    {
        var productsEntities = await _productRepository.GetById(id);

        if (productsEntities is null) throw new KeyNotFoundException("Produto não encontrada");

        await _productRepository.Delete(productsEntities.ProductId);
    }
}
