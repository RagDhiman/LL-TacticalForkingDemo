using ShopDomain.DataAccess;

namespace ShopDomain.Model
{
    public class OrderHistory: IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}