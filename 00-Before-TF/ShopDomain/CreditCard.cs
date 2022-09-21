namespace ShopDomain
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public int AccountId { get; set; }
        public string CreditCardName { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime CreditDate { get; set; }
        public Account Account { get; set; }
    }
}