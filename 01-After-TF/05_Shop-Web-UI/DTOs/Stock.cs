namespace Shop_Web_UI.DTOs
{
    public class Stock : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int InventoryCheckId { get; set; }
    }
}