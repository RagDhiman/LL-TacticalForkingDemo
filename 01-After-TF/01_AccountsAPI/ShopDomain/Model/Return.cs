using ShopDomain.DataAccess;

namespace ShopDomain.Model
{
    public class Return : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Refund { get; set; }
        public Order Order { get; set; }
    }
}