namespace ShopDomain
{
    public class Delivery
    {
        public int OrderId { get; set; }
        public int DeliveryId { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
        public Order Order { get; set; }
    }
}