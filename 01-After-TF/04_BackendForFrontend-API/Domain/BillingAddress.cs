using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class BillingAddress: IEntity
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}