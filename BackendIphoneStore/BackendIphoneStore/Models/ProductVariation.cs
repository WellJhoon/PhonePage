using System.ComponentModel.DataAnnotations;

namespace BackendIphoneStore.Models
{
    public class ProductVariation
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Color { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        public int Stock { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}