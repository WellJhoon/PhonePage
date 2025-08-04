using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendIphoneStore.Data;
using BackendIphoneStore.DTOs;
using BackendIphoneStore.Models;

namespace BackendIphoneStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var query = _context.Products.Include(p => p.Variations).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    description = p.Description,
                    imageUrl = p.ImageUrl,
                    createdAt = p.CreatedAt,
                    variations = p.Variations.Select(v => new
                    {
                        id = v.Id,
                        color = v.Color,
                        price = v.Price,
                        stock = v.Stock
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new
            {
                products,
                totalCount,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Variations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                imageUrl = product.ImageUrl,
                createdAt = product.CreatedAt,
                variations = product.Variations.Select(v => new
                {
                    id = v.Id,
                    color = v.Color,
                    price = v.Price,
                    stock = v.Stock
                }).ToList()
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            foreach (var variationDto in productDto.Variations)
            {
                var variation = new ProductVariation
                {
                    ProductId = product.Id,
                    Color = variationDto.Color,
                    Price = variationDto.Price,
                    Stock = variationDto.Stock
                };
                _context.ProductVariations.Add(variation);
            }

            await _context.SaveChangesAsync();

            var createdProduct = await _context.Products
                .Include(p => p.Variations)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new
            {
                id = createdProduct.Id,
                name = createdProduct.Name,
                description = createdProduct.Description,
                imageUrl = createdProduct.ImageUrl,
                variations = createdProduct.Variations.Select(v => new
                {
                    id = v.Id,
                    color = v.Color,
                    price = v.Price,
                    stock = v.Stock
                }).ToList()
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            var product = await _context.Products
                .Include(p => p.Variations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.ImageUrl = productDto.ImageUrl;
            product.UpdatedAt = DateTime.UtcNow;

            _context.ProductVariations.RemoveRange(product.Variations);

            foreach (var variationDto in productDto.Variations)
            {
                var variation = new ProductVariation
                {
                    ProductId = product.Id,
                    Color = variationDto.Color,
                    Price = variationDto.Price,
                    Stock = variationDto.Stock
                };
                _context.ProductVariations.Add(variation);
            }

            await _context.SaveChangesAsync();

            var updatedProduct = await _context.Products
                .Include(p => p.Variations)
                .FirstOrDefaultAsync(p => p.Id == id);

            return Ok(new
            {
                id = updatedProduct.Id,
                name = updatedProduct.Name,
                description = updatedProduct.Description,
                imageUrl = updatedProduct.ImageUrl,
                variations = updatedProduct.Variations.Select(v => new
                {
                    id = v.Id,
                    color = v.Color,
                    price = v.Price,
                    stock = v.Stock
                }).ToList()
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}