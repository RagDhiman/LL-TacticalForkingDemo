namespace ShopWebAPI.Model
{
    public class OrderHistoryModel
    {
        public int OrderHistoryId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}