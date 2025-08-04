using System.ComponentModel.DataAnnotations;

namespace BackendIphoneStore.DTOs
{
    public class ProductDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        
        [StringLength(500)]
        public string ImageUrl { get; set; }
        
        public List<ProductVariationDto> Variations { get; set; } = new List<ProductVariationDto>();
    }

    public class ProductVariationDto
    {
        [Required]
        [StringLength(50)]
        public string Color { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        
        public int Stock { get; set; } = 0;
    }
}