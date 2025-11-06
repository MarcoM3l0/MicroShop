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

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoriesEntities = await _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntities);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    {
        var categoriesEntities = await _categoryRepository.GetCategoriesProducts();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntities);
    }

    public async Task<CategoryDTO?> GetCategoryById(int id)
    {
        var categoryEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO?>(categoryEntity);
    }

    public async Task AddCategory(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Create(categoryEntity);
        categoryDto.CategoryId = categoryEntity.CategoryId;
    }

    public async Task UpdateCategory(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Update(categoryEntity.CategoryId, categoryEntity);
    }

    public async Task DeleteCategory(int id)
    {
        var categoryEntity = _categoryRepository.GetById(id);

        if (categoryEntity is null) throw new KeyNotFoundException("Categoria não encontrada");

        await _categoryRepository.Delete(id);
    }
}
