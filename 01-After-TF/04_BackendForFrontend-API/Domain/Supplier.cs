using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class Supplier : IEntity
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
        public string TelephoneNo { get; set; }
    }
}