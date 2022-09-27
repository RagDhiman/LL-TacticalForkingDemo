using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class OrderHistoryModel
    {
        public int Id { get; set; }

        [Display(Name = "Order Ref.")]
        public int OrderId { get; set; }

        [Display(Name = "Order Date.")]
        public DateTime OrderDate { get; set; }

    }
}