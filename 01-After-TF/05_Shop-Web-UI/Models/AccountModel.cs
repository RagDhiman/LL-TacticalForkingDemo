using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class AccountModel
    {
        [Display(Name = "Account No.")]
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Open Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
    }
}