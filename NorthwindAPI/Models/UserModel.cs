using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NorthwindAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public string? Username { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [DefaultValue("System")]
        public string UserName { get; set; }

        [Required]
        [DefaultValue("System")]
        public string Password { get; set; }
    }

    public class AuthenticationResponseModel
    {
        public string? FullName { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }

        public AuthenticationResponseModel(UserModel userDetail, string token)
        {
            Token = token;
            FullName = userDetail.FirstName + " " + userDetail.LastName;
            UserId = userDetail.UserId;
        }
        //{ "Fullname" : "xxxx", "UserId" : 99, "TOken":"EncToken" } -> this is our output json
    }
}
