using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class CreditCard: IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CreditCardName { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime CreditDate { get; set; }
        public Account Account { get; set; }
    }
}