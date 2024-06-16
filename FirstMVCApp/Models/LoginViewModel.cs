using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username required")]
        public string Username { get; set; } = "";
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; } = "";
        public bool rememberMe { get; set; }
    }
}
