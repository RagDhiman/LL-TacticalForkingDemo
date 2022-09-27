

using System.ComponentModel.DataAnnotations;

namespace Shop_Web_UI.Models
{
    public class AddressModel
    {
        public int Id { get; set; }

        [Display(Name = "Account No.")]
        public int AccountId { get; set; }

        [Display(Name = "Address Line (1)")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line (2)")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line (3)")]
        public string AddressLine3 { get; set; }

        [Display(Name = "City Town")]
        public string CityTown { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
    }
}