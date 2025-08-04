using Microsoft.EntityFrameworkCore;
using BackendIphoneStore.Models;
using BCrypt.Net;

namespace BackendIphoneStore.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User
                    {
                        Username = "admin",
                        Email = "admin@iphone.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = UserRole.Admin
                    },
                    new User
                    {
                        Username = "seller",
                        Email = "seller@iphone.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("seller123"),
                        Role = UserRole.Seller
                    },
                    new User
                    {
                        Username = "user",
                        Email = "user@iphone.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.User
                    }
                };

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product
                    {
                        Name = "iPhone 15 Pro",
                        Description = "El iPhone más avanzado con chip A17 Pro y cámara de 48MP",
                        ImageUrl = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMTAwJSIgaGVpZ2h0PSIxMDAlIiBmaWxsPSIjMTk3NmQyIi8+PHRleHQgeD0iNTAlIiB5PSI1MCUiIGZvbnQtZmFtaWx5PSJBcmlhbCIgZm9udC1zaXplPSIxOCIgZmlsbD0id2hpdGUiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5pUGhvbmUgMTUgUHJvPC90ZXh0Pjwvc3ZnPg=="
                    },
                    new Product
                    {
                        Name = "iPhone 15",
                        Description = "iPhone con Dynamic Island y cámara principal de 48MP",
                        ImageUrl = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMzAwIiBoZWlnaHQ9IjIwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iMTAwJSIgaGVpZ2h0PSIxMDAlIiBmaWxsPSIjNGNhZjUwIi8+PHRleHQgeD0iNTAlIiB5PSI1MCUiIGZvbnQtZmFtaWx5PSJBcmlhbCIgZm9udC1zaXplPSIxOCIgZmlsbD0id2hpdGUiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGR5PSIuM2VtIj5pUGhvbmUgMTU8L3RleHQ+PC9zdmc+"
                    }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();

                var variations = new ProductVariation[]
                {
                    new ProductVariation { ProductId = 1, Color = "Negro", Price = 1200, Stock = 10 },
                    new ProductVariation { ProductId = 1, Color = "Azul", Price = 1200, Stock = 8 },
                    new ProductVariation { ProductId = 1, Color = "Verde", Price = 1300, Stock = 5 },
                    new ProductVariation { ProductId = 2, Color = "Negro", Price = 1000, Stock = 15 },
                    new ProductVariation { ProductId = 2, Color = "Rosa", Price = 1000, Stock = 12 },
                    new ProductVariation { ProductId = 2, Color = "Azul", Price = 1050, Stock = 7 }
                };

                context.ProductVariations.AddRange(variations);
                await context.SaveChangesAsync();
            }
        }
    }
}