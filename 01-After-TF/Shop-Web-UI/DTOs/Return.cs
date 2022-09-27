namespace Shop_Web_UI.DTOs
{
    public class Return : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Refund { get; set; }
    }
}