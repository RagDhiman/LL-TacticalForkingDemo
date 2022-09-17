namespace ShopDomain
{
    public class BillingAddress
    {
        public int BillingAddressId { get; set; }
        public int CreditCardId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}