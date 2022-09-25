

namespace Shop_UI_Web.Data.DTOs
{
    public class CreditCardModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CreditCardName { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime CreditDate { get; set; }
    }
}