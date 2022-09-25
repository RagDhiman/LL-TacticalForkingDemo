namespace Shop_Web_UI.Models
{
    public class ReturnModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Refund { get; set; }
    }
}