using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")] // Route path: /api/products
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    // Dependency Injection injects AppDbContext here
    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // 1. READ ALL: GET /api/products
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }

    // 2. READ BY ID: GET /api/products/1 (Route Parameter)
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }
        return Ok(product);
    }

    // 3. CREATE: POST /api/products (Request Body)
    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();

        // Returns HTTP 201 Created with a Location header pointing to GetById
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // 4. UPDATE: PUT /api/products/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Product updatedProduct)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;

        _context.SaveChanges();
        return NoContent(); // Standard HTTP 204 for successful updates
    }

    // 5. DELETE: DELETE /api/products/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent(); // Standard HTTP 204 for successful deletions
    }
}