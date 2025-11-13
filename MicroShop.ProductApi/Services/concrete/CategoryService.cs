using AutoMapper;
using MicroShop.ProductApi.DTOs;
using MicroShop.ProductApi.Models;
using MicroShop.ProductApi.Repositories;
using MicroShop.ProductApi.Services.interfaces;

namespace MicroShop.ProductApi.Services.concrete;

public class CategoryService : ICategoryService
{

    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Serviço responsável por buscar todas as categorias disponíveis no sistema.
    /// </summary>
    /// <returns>
    /// Uma lista de objetos <see cref="CategoryDTO"/> contendo os dados das categorias.
    /// </returns>
    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoriesEntities = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntities);
    }

    /// <summary>
    /// Recupera todas as categorias com seus respectivos produtos.
    /// </summary>
    /// <returns>
    /// Uma lista de objetos <see cref="CategoryDTO"/>, cada um contendo os dados da categoria
    /// e a lista de produtos associados.
    /// </returns>
    /// <remarks>
    /// Cada produto também contém uma referência à sua categoria, o que pode gerar circularidade
    /// se não for tratado corretamente na serialização.
    /// </remarks>
    /// <example>
    /// Exemplo de retorno:
    /// [
    ///   {
    ///     "categoryId": 1,
    ///     "name": "Material Escolar",
    ///     "products": [
    ///       {
    ///         "productId": 101,
    ///         "name": "Caderno Espiral",
    ///         "price": 12.99,
    ///         "description": "Caderno com 200 folhas",
    ///         "stock": 50,
    ///         "imageURL": "https://exemplo.com/imagens/caderno.jpg",
    ///         "categoryId": 1,
    ///         "category": {
    ///           "categoryId": 1,
    ///           "name": "Material Escolar",
    ///           "products": [ "..." ]
    ///         }
    ///       }
    ///     ]
    ///   }
    /// ]
    /// </example>
    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    {
        var categoriesEntities = await _categoryRepository.GetCategoriesProducts();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntities);
    }

    /// <summary>
    /// Recupera uma categoria específica com base no identificador fornecido, incluindo seus produtos associados.
    /// </summary>
    /// <param name="id">O identificador único da categoria.</param>
    /// <returns>
    /// Um objeto <see cref="CategoryDTO"/> contendo os dados da categoria e seus produtos,
    /// ou <c>null</c> se a categoria não for encontrada.
    /// </returns>
    /// <remarks>
    /// Cada produto também inclui uma referência à sua categoria, o que pode gerar circularidade na serialização.
    /// Certifique-se de configurar o mapeamento ou a serialização para evitar loops infinitos.
    /// </remarks>
    /// <example>
    /// Exemplo de retorno:
    /// {
    ///   "categoryId": 1,
    ///   "name": "Material Escolar",
    ///   "products": [
    ///     {
    ///       "productId": 101,
    ///       "name": "Caderno Espiral",
    ///       "price": 12.99,
    ///       "description": "Caderno com 200 folhas",
    ///       "stock": 50,
    ///       "imageURL": "https://exemplo.com/imagens/caderno.jpg",
    ///       "categoryId": 1,
    ///       "category": {
    ///         "categoryId": 1,
    ///         "name": "Material Escolar",
    ///         "products": [ "..." ]
    ///       }
    ///     }
    ///   ]
    /// }
    /// </example>
    public async Task<CategoryDTO?> GetCategoryById(int id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO?>(categoryEntity);
    }

    /// <summary>
    /// Adiciona uma nova categoria ao sistema, incluindo seus produtos associados.
    /// </summary>
    /// <param name="categoryDto">
    /// Objeto <see cref="CategoryDTO"/> contendo os dados da categoria e, opcionalmente, os produtos vinculados.
    /// </param>
    /// <remarks>
    /// Após a criação, o identificador gerado para a categoria é atribuído de volta ao <c>categoryDto.CategoryId</c>.
    /// </remarks>
    public async Task AddCategory(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Create(categoryEntity);
        categoryDto.CategoryId = categoryEntity.CategoryId;
    }

    /// <summary>
    /// Atualiza os dados de uma categoria existente no sistema.
    /// </summary>
    /// <param name="categoryDto">
    /// Objeto <see cref="CategoryDTO"/> contendo os dados atualizados da categoria,
    /// incluindo seus produtos, se aplicável. O campo <c>CategoryId</c> deve estar preenchido.
    /// </param>
    /// <remarks>
    /// O método realiza o mapeamento do DTO para a entidade e executa a atualização no repositório.
    /// Certifique-se de que a categoria já exista antes de chamar este método.
    /// </remarks>
    public async Task UpdateCategory(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Update(categoryEntity.CategoryId, categoryEntity);
    }

    /// <summary>
    /// Remove uma categoria existente com base no identificador fornecido.
    /// </summary>
    /// <param name="id">O identificador único da categoria a ser removida.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a categoria com o ID especificado não é encontrada no repositório.
    /// </exception>
    /// <remarks>
    /// Antes da exclusão, o método verifica se a categoria existe. Caso contrário, uma exceção é lançada.
    /// </remarks>

    public async Task DeleteCategory(int id)
    {
        var categoryEntity = _categoryRepository.GetById(id)
            ?? throw new KeyNotFoundException("Categoria não encontrada");

        await _categoryRepository.Delete(id);
    }
}
