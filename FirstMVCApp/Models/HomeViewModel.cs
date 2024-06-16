using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMVCApp.Models
{
    public class HomeViewModel
    {
        public string FirstName { get; set; }
        public bool IsRegistered { get; set; }
        public string SelectedCountry { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public HomeViewModel()
        {
            Countries = new List<SelectListItem>()
            {
                new SelectListItem { Text = "India", Value = "India" },
                new SelectListItem { Text = "Japan", Value = "Japan" },
                new SelectListItem { Text = "Italy", Value = "Italy" },
                new SelectListItem { Text = "China", Value = "China" },

            };
            FirstName = string.Empty;
            SelectedCountry = string.Empty;
            IsRegistered = false;
        }

    }
}
