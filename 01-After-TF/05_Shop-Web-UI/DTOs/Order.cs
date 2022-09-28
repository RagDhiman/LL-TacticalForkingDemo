namespace Shop_Web_UI.DTOs
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}