namespace Shop_BackendForFrontend_API.Model
{
    public class DeliveryModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
    }
}