using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class StockModel
    {
        [Display(Name = "Stock Ref.")]
        public int Id { get; set; }

        [Display(Name = "Product Ref.")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Inventory Check Ref.")]
        public int InventoryCheckId { get; set; }
    }
}