using ShopDomain.DataAccess;

namespace ShopWebAPI.Model
{
    public class CreditCardModel: IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CreditCardName { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime CreditDate { get; set; }
    }
}