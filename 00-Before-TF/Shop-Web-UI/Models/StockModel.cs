namespace Shop_Web_UI.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int InventoryCheckId { get; set; }
    }
}