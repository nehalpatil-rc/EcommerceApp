namespace EcommerceApp.Models
{
    public class CartItem
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
