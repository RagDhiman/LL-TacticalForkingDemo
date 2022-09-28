namespace Shop_Web_UI.DTOs
{
    public class OrderHistory : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}