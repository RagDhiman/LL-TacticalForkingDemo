

namespace Shop_UI_Web.Data.DTOs
{
    public class InventoryCheckModel
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public DateTime CheckDateTime { get; set; }
        public string InspectorName { get; set; }
    }
}