using Shop_BackendForFrontend_API.Data;

namespace Shop_BackendForFrontend_API.Domain
{
    public class Order: IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Account Account { get; set; }

        public Product Product { get; set; }
    }
}