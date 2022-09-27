namespace Shop_Web_UI.DTOs
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CreditCardName { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime CreditDate { get; set; }
    }
}