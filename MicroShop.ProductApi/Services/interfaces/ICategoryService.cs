using MicroShop.ProductApi.DTOs;

namespace MicroShop.ProductApi.Services.interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
    Task<CategoryDTO?> GetCategoryById(int id);
    Task AddCategory(CategoryDTO categoryDto);
    Task UpdateCategory(CategoryDTO categoryDto);
    Task DeleteCategory(int id);
}