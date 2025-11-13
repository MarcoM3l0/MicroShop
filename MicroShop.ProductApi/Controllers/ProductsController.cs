using MicroShop.ProductApi.DTOs;
using MicroShop.ProductApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.ProductApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        try
        {
            var productsDto = await _productService.GetProducts();
            if (productsDto is null) return NotFound("Produtos não encontrados");
            return Ok(productsDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao recuperar dados do banco de dados");
        }
    }

    [HttpGet("{id}", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        try
        {
            var productDto = await _productService.GetProductById(id);
            if (productDto is null) return NotFound("Produto não encontrado");
            return Ok(productDto);
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
    public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
    {
        try
        {
            if (productDto is null) return BadRequest("Dados inválidos");
            await _productService.AddProduct(productDto);
            return new CreatedAtRouteResult("GetProduct",
                new { id = productDto.ProductId }, productDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar dados no banco de dados");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
    {
        try
        {
            if (id != productDto.ProductId) return NotFound("Produto não encontrado");
            if (productDto is null) return BadRequest("Dados inválidos");
            await _productService.UpdateProduct(productDto);
            return Ok(productDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar o produto");
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
            var productDto = await _productService.GetProductById(id);

            if (productDto is null) return NotFound("Produto não encontrado");

            await _productService.DeleteProduct(id);
            return Ok(productDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar deletar o produto");
        }
    }
}
