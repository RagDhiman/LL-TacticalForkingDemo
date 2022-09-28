using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class InventoryCheck : IEntity
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public DateTime CheckDateTime { get; set; }
        public string InspectorName { get; set; }
    }
}