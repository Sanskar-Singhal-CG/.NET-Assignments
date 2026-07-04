using Microsoft.AspNetCore.Mvc;
using Catalog.API.Models;
using Catalog.API.Dtos;
using Catalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(CreateProductDto dto)
    {
        var product = new Product(0, dto.Name, dto.Price, dto.Category);
        var createdProduct = await _repository.CreateAsync(product);
        return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _repository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
