using MicroShop.ProductApi.DTOs;
using MicroShop.ProductApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.ProductApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        try
        {
            var categoriesDto = await _categoryService.GetCategories();

            if (categoriesDto is null) return NotFound("Categorias não encontradas");

            return Ok(categoriesDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpGet("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
    {
        try
        {
            var categoriesDto = await _categoryService.GetCategoriesProducts();

            if (categoriesDto is null) return NotFound("Categorias não encontradas");

            return Ok(categoriesDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpGet("{id}", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        try
        {
            var categoryDto = await _categoryService.GetCategoryById(id);

            if (categoryDto is null) return NotFound();

            return Ok(categoryDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
    {
        try
        {
            if (categoryDto is null) return BadRequest("Dados inválidos");

            await _categoryService.AddCategory(categoryDto);

            return new CreatedAtRouteResult("GetCategory",
                new { id = categoryDto.CategoryId }, categoryDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova categoria");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
    {
        try
        {
            if (id != categoryDto.CategoryId) return NotFound("Categoria não encontrada");
            if (categoryDto is null) return BadRequest("Dados inválidos");

            await _categoryService.UpdateCategory(categoryDto);

            return Ok(categoryDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar a categoria");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var categoryDto = await _categoryService.GetCategoryById(id);

            if (categoryDto is null) return NotFound("Categoria não encontrada");

            await _categoryService.DeleteCategory(id);
            return Ok(categoryDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar excluir a categoria");
        }
    }
}
