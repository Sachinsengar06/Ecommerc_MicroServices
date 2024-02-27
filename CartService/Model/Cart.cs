namespace CartService.Model
{
    public class Cart
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductName { get; set; } = "";

        public int Price { get; set; }
    }
}
