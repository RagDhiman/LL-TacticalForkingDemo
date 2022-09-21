namespace ShopDomain
{
    public class Order
    {
        public int OrderId { get; set; }

        public int AccountId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Account Account { get; set; }

        public Product Product { get; set; }
    }
}