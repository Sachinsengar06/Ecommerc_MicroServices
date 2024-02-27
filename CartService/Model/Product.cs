namespace CartService.Model
{
    public class Product
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSize { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQty { get; set; }
    }
}
