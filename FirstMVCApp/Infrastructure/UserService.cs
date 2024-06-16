using FirstMVCApp.Models;
using Microsoft.Data.SqlClient;

namespace FirstMVCApp.Infrastructure
{
    public interface IUserService
    {
        bool Authenticate(LoginViewModel model);
        bool SignOut();
    }
    public class UserService : IUserService
    {
        /*private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }*/
        public bool Authenticate(LoginViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(@"server=(local);database=CaseStudy1;integrated security=sspi;trustservercertificate=true"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var sql = "SELECT * FROM Users where Username=@user And Password=@pwd";
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = sql;

                    // Add parameters to avoid SQL injection vulnerabilities
                    command.Parameters.AddWithValue("@user", model.Username);
                    command.Parameters.AddWithValue("@pwd", model.Password);

                    var status = command.ExecuteReader().HasRows;
                    return status;
                }
            }
        }


        public bool SignOut()
        {
            //SignOut the user from the Db, assuming the sign-in and sing-out times are being maintained.
            return true;
        }
    }
}
