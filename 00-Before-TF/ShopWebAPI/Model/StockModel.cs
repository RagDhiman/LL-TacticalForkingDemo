namespace ShopWebAPI.Model
{
    public class StockModel
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int InventoryCheckId { get; set; }
    }
}