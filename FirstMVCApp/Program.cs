using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;

namespace FirstMVCApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connStr = builder.Configuration.GetConnectionString("NorthwindConnection");

            // Add services to the container.
            //builder.Services.AddDbContext<ProductsDbContextcs>();
            builder.Services.AddDbContext<ProductsDbContextcs>(optionsAction =>
            {
                optionsAction.UseSqlServer(connectionString: connStr);
            });
            //builder.Services.AddScoped<IRepository<Product,int>,ProductListRepository>();
            builder.Services.AddScoped<IRepository<Product,int>,ProductEFRepository>();//THis is for dependency inversion;
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme
                ).AddCookie(cookieOptions =>
                {
                    //cookieOptions.Cookie.HttpOnly = true;
                    //options.ExpireTimeSpan
                    cookieOptions.LoginPath = "/accounts/signin";//Login Path = unauthenticated users will be redirected to this path
                    cookieOptions.LogoutPath = "/accounts/logout";
                    // (creation of persistent cookie) cookieOptions.Cookie.Expiration = 
                });

            builder.Services.AddSession();


            //Application Dependencies which will be injected as and when required.
            builder.Services.AddControllersWithViews();
            builder.Services.AddResponseCaching();

            //HTTP request pipeline configurations
            // It is a collection of various middlewares which are injected into hte request processing pipeline
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseResponseCaching();

            //URL - http://localhost:1234/css/first.css
            //when the client requests for .html/.css/.js files, it will be handled by this code block
            //UseStaticFiles searches for the requested content in the wwwroot folder
            app.UseStaticFiles();

            app.UseMiddleware<CustomMiddleware>();

            // URL - http://local:1234/products/list (since no extension, we need to write here)
            app.UseRouting();

            //restricts the requests to authorized users only
            app.UseAuthorization();

            app.UseSession();

            //splits the url into various segments and tries to find the matching handlers based on given pattern
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            //last middleware in the pipeline which will execute the matched code blocks and generate response
            app.Run();

            //no middleware attached after run
        }
    }
    public class CustomMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger _logger;
        public CustomMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;
            _logger = logger.CreateLogger(typeof(CustomMiddleware));
            
        }
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("CustomMiddleware executing...");
            //precontroller execution
            await _next(context);
            //adding items to a context
            //context.Items["Key"] = "value";
            //post controller -action-view engine execution
            //context.Response.WriteAsync("Hello world");
            //context.Request.Body;
            //context.Response.Body;
            //context.Items;
        }
    }
}
