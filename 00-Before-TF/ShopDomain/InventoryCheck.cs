namespace ShopDomain
{
    public class InventoryCheck
    {
        public int InventoryCheckId { get; set; }

        public int StockId { get; set; }    
        public DateTime CheckDateTime { get; set; }
        public string InspectorName { get; set; }
    }
}