using ShopDomain.DataAccess;

namespace ShopWebAPI.Model
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string EmailAddress { get; set; }
    }
}