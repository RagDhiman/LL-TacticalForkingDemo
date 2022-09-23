namespace ShopDomain.Model
{
    public class Return
    {
        public int ReturnId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Refund { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}