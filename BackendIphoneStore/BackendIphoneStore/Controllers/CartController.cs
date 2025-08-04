using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendIphoneStore.Data;
using BackendIphoneStore.DTOs;
using BackendIphoneStore.Models;
using System.Security.Claims;

namespace BackendIphoneStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            var cart = await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(ci => ci.ProductVariation)
                        .ThenInclude(pv => pv.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return Ok(new CartResponseDto { Items = new List<CartItemDto>(), Total = 0 });
            }

            var cartDto = new CartResponseDto
            {
                Id = cart.Id,
                Items = cart.Items.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductVariationId = ci.ProductVariationId,
                    ProductName = ci.ProductVariation.Product.Name,
                    Color = ci.ProductVariation.Color,
                    Price = ci.ProductVariation.Price,
                    Quantity = ci.Quantity,
                    Subtotal = ci.ProductVariation.Price * ci.Quantity
                }).ToList()
            };

            cartDto.Total = cartDto.Items.Sum(i => i.Subtotal);

            return Ok(cartDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            var userId = GetUserId();
            
            // Verificar stock disponible
            var productVariation = await _context.ProductVariations
                .FirstOrDefaultAsync(pv => pv.Id == addToCartDto.ProductVariationId);
            
            if (productVariation == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }
            
            if (productVariation.Stock < addToCartDto.Quantity)
            {
                return BadRequest(new { message = $"Stock insuficiente. Solo quedan {productVariation.Stock} unidades" });
            }
            
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductVariationId == addToCartDto.ProductVariationId);

            if (existingItem != null)
            {
                if (productVariation.Stock < existingItem.Quantity + addToCartDto.Quantity)
                {
                    return BadRequest(new { message = $"Stock insuficiente. Solo quedan {productVariation.Stock} unidades" });
                }
                existingItem.Quantity += addToCartDto.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductVariationId = addToCartDto.ProductVariationId,
                    Quantity = addToCartDto.Quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Producto agregado al carrito" });
        }

        [HttpDelete("item/{itemId}")]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var userId = GetUserId();
            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == itemId && ci.Cart.UserId == userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            // No devolvemos stock al eliminar del carrito, solo al hacer checkout se reduce
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Producto eliminado del carrito" });
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = GetUserId();
            var cart = await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(ci => ci.ProductVariation)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
            {
                return BadRequest(new { message = "El carrito está vacío" });
            }

            // Verificar stock antes de procesar la compra
            foreach (var item in cart.Items)
            {
                if (item.ProductVariation.Stock < item.Quantity)
                {
                    return BadRequest(new { message = $"Stock insuficiente para {item.ProductVariation.Color}. Solo quedan {item.ProductVariation.Stock} unidades" });
                }
            }

            // Reducir stock
            foreach (var item in cart.Items)
            {
                var variation = await _context.ProductVariations.FindAsync(item.ProductVariationId);
                if (variation != null)
                {
                    variation.Stock -= item.Quantity;
                    Console.WriteLine($"Reduciendo stock de {variation.Color}: {variation.Stock + item.Quantity} -> {variation.Stock}");
                }
            }

            // Limpiar carrito
            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();

            return Ok(new { message = "¡Felicidades por su compra falsa! 🎉" });
        }
    }
}