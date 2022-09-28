

using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Display(Name = "Account No.")]
        public int AccountId { get; set; }

        [Display(Name = "Product Ref.")]
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}