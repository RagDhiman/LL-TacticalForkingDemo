using ShopDomain.DataAccess;

namespace ShopWebAPI.Model
{
    public class AddressModel: IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityTown { get; set; }
        public string PostCode { get; set; }
    }
}