using Unity;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;
using NuGet.Packaging.Core;
using NorthwindAPI.Services;
//using Unity.Microsoft.DependencyInjection;

namespace NorthwindAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.AddScoped<IUserServiceAsync,UserService>();
            
            
            //var connStr = builder.Configuration.GetConnectionString("NorthwindConnection");

            // Add services to the container.
            builder.Services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseSqlServer(@"server=(local);database=northwind;integrated security=sspi;trustservercertificate=true");
            });

            builder.Services.AddControllers(configuration =>
            {
                // global level filters on all the controllers/actions
                //configuration.Filters.Add(new CustomAuthorizationAttribute());
            });

            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin() // *
                .AllowAnyMethod() // GET, PUT, POST, HEAD, TRACE,OPTIONS, CONNECT
                .AllowAnyHeader(); // ACCEPT, CONTENT-TYPE,  AUTHORIZATION, KEEP-ALIVE...
            });

            //app.UserAuthentication();
            app.UseAuthorization();

            /*app.UseMiddleware<JwtMiddleware>();*/


            app.MapControllers();

            app.Run();
        }
    }
}
