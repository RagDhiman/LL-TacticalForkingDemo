using ShopDomain.DataAccess;

namespace ShopWebAPI.Model
{
    public class InventoryCheckModel
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public DateTime CheckDateTime { get; set; }
        public string InspectorName { get; set; }
    }
}