

namespace Shop_Web_UI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}