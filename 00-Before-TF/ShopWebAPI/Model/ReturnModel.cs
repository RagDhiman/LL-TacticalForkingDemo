namespace ShopWebAPI.Model
{
    public class ReturnModel
    {
        public int ReturnId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Refund { get; set; }
    }
}