namespace ShopDomain.Model
{
    public class OrderHistory
    {
        public int OrderHistoryId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}