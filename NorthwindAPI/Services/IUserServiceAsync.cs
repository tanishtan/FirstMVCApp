using NorthwindAPI.Models;

namespace NorthwindAPI.Services
{
    public interface IUserServiceAsync
    {
        Task<UserModel> AuthenticateAsync(LoginModel model);
    }

    public class UserService : IUserServiceAsync
    {
        private List<UserModel> _users = new List<UserModel>
        {
            new UserModel{UserId=1,FirstName="Ruben",LastName="Gupta", RoleName="Admin", Email="ruben.gupta@email.com",Username="admin",Password="admin"},
            new UserModel{UserId=2,FirstName="Sharan",LastName="Purohit", RoleName="Operator", Email="sharan.purohit@email.com",Username="operator",Password="operator"}
        };

        public Task<UserModel> AuthenticateAsync(LoginModel model)
        {
            var user = _users.FirstOrDefault(c=>c.Username == model.UserName && c.Password== model.Password);
            /*if (user is not null)
                return Task.FromResult(user);
            else
                return default!;*/
            return Task.Run(() => user);
        }
    }
}
