namespace Shop_BackendForFrontend_API.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public decimal ProductPrice { get; set; }
        public int SupplierId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}