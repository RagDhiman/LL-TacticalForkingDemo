namespace Shop_Web_UI.DTOs
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal ProductPrice { get; set; }
        public int SupplierId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}