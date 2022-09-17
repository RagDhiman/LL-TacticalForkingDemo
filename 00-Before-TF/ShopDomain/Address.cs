namespace ShopDomain
{
    public class Address
    {
        public int AddressId { get; set; }
        public int AccountId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
        public Account Account { get; set; }

    }
}