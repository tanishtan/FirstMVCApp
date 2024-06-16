using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Customer Id is Required")]
        [MaxLength(5,ErrorMessage = "The maximum length should be 5")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Company Name is Required")]
        [MaxLength (50, ErrorMessage = "The maximum length should be 50")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Contact Name is Required")]
        [MaxLength(50, ErrorMessage = "The maximum length should be 50")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "City is Required")]
        [MaxLength(25, ErrorMessage = "The maximum length should be 25")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public Customer()
        {
            Countries = new List<SelectListItem>()
            {
                new SelectListItem { Text = "India", Value = "India" },
                new SelectListItem { Text = "Japan", Value = "Japan" },
                new SelectListItem { Text = "Italy", Value = "Italy" },
                new SelectListItem { Text = "China", Value = "China" },
                new SelectListItem { Text = "Germany", Value = "Germany" },
            };
        }
    }
}
