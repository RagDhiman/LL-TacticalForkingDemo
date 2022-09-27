using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class ReturnModel
    {
        public int Id { get; set; }

        [Display(Name = "Order Ref.")]
        public int OrderId { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        public bool Refund { get; set; }
    }
}