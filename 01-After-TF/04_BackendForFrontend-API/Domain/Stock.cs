using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class Stock : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public int InventoryCheckId { get; set; }
    }
}