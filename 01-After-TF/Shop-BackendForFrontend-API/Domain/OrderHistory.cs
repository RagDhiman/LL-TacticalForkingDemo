using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class OrderHistory: IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}