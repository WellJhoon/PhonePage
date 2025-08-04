namespace BackendIphoneStore.DTOs
{
    public class AddToCartDto
    {
        public int ProductVariationId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class CartResponseDto
    {
        public int Id { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public decimal Total { get; set; }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductVariationId { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}