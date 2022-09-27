

using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class InventoryCheckModel
    {
        public int Id { get; set; }

        [Display(Name = "Stock Ref.")]
        public int StockId { get; set; }

        [Display(Name = "Check Date")]
        public DateTime CheckDateTime { get; set; }

        [Display(Name = "Inspector Name")]
        public string InspectorName { get; set; }
    }
}