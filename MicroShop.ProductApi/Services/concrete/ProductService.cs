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

    /// <summary>
    /// Recupera todos os produtos disponíveis no sistema.
    /// </summary>
    /// <returns>
    /// Uma lista de objetos <see cref="ProductDTO"/> contendo os dados dos produtos,
    /// incluindo informações como nome, preço, descrição, estoque, imagem e categoria associada.
    /// </returns>
    /// <example>
    /// Exemplo de retorno:
    /// [
    ///   {
    ///     "productId": 101,
    ///     "name": "Caderno Espiral",
    ///     "price": 12.99,
    ///     "description": "Caderno com 200 folhas",
    ///     "stock": 50,
    ///     "imageURL": "https://exemplo.com/imagens/caderno.jpg",
    ///     "categoryId": 1
    ///   },
    ///   {
    ///     "productId": 102,
    ///     "name": "Fone de Ouvido Bluetooth",
    ///     "price": 89.90,
    ///     "description": "Fone com cancelamento de ruído",
    ///     "stock": 20,
    ///     "imageURL": "https://exemplo.com/imagens/fone.jpg",
    ///     "categoryId": 2
    ///   }
    /// ]
    /// </example>

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsEntities = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(productsEntities);
    }

    /// <summary>
    /// Recupera os dados de um produto específico com base no identificador fornecido.
    /// </summary>
    /// <param name="id">O identificador único do produto.</param>
    /// <returns>
    /// Um objeto <see cref="ProductDTO"/> contendo os dados do produto, incluindo nome, preço, descrição,
    /// estoque, imagem e categoria associada, ou <c>null</c> se o produto não for encontrado.
    /// </returns>
    /// <example>
    /// Exemplo de retorno:
    /// {
    ///   "productId": 101,
    ///   "name": "Caderno Espiral",
    ///   "price": 12.99,
    ///   "description": "Caderno com 200 folhas",
    ///   "stock": 50,
    ///   "imageURL": "https://exemplo.com/imagens/caderno.jpg",
    ///   "categoryId": 1
    /// }
    /// </example>
    public async Task<ProductDTO?> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO?>(productEntity);
    }

    /// <summary>
    /// Adiciona um novo produto ao sistema, vinculando-o à categoria correspondente.
    /// </summary>
    /// <param name="productDto">
    /// Objeto <see cref="ProductDTO"/> contendo os dados do produto a ser criado,
    /// incluindo nome, preço, descrição, estoque, imagem e o identificador da categoria.
    /// </param>
    /// <remarks>
    /// Após a criação, o identificador gerado para o produto é atribuído de volta ao <c>productDto.ProductId</c>.
    /// </remarks>
    public async Task AddProduct(ProductDTO productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Create(productEntity);
        productDto.ProductId = productEntity.ProductId;
    }

    /// <summary>
    /// Atualiza os dados de um produto existente no sistema.
    /// </summary>
    /// <param name="productDto">
    /// Objeto <see cref="ProductDTO"/> contendo os dados atualizados do produto,
    /// incluindo nome, preço, descrição, estoque, imagem e categoria associada.
    /// O campo <c>ProductId</c> deve estar preenchido.
    /// </param>
    /// <remarks>
    /// O método realiza o mapeamento do DTO para a entidade e executa a atualização no repositório.
    /// Certifique-se de que o produto já exista antes de chamar este método.
    /// </remarks>
    public async Task UpdateProduct(ProductDTO productDto)
    {
        var productEntity = _mapper.Map<Product>(productDto);
        await _productRepository.Update(productEntity.ProductId, productEntity);
    }

    /// <summary>
    /// Remove um produto existente com base no identificador fornecido.
    /// </summary>
    /// <param name="id">O identificador único do produto a ser removido.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando o produto com o ID especificado não é encontrado no repositório.
    /// </exception>
    /// <remarks>
    /// O método verifica se o produto existe antes de realizar a exclusão.
    /// Caso não seja encontrado, uma exceção é lançada.
    /// </remarks>
    public async Task DeleteProduct(int id)
    {
        var productsEntities = await _productRepository.GetById(id);

        if (productsEntities is null) throw new KeyNotFoundException("Produto não encontrada");

        await _productRepository.Delete(productsEntities.ProductId);
    }
}
