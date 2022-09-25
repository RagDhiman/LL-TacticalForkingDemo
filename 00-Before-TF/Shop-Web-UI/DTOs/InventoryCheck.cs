namespace Shop_Web_UI.DTOs
{
    public class InventoryCheck : IEntity
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public DateTime CheckDateTime { get; set; }
        public string InspectorName { get; set; }
    }
}