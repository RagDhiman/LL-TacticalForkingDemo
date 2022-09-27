

using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class CreditCardModel
    {
        public int Id { get; set; }

        [Display(Name = "Account No.")]
        public int AccountId { get; set; }

        [Display(Name = "Vendor")]
        public string CreditCardName { get; set; }

        [Display(Name = "Number")]
        public int CreditCardNumber { get; set; }

        [Display(Name = "Start Date")]
        public DateTime CreditDate { get; set; }
    }
}