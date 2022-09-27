using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Supplier Ref.")]
        public int SupplierId { get; set; }

        [Display(Name = "In Stock Date")]
        public DateTime CreatedDate { get; set; }
    }
}