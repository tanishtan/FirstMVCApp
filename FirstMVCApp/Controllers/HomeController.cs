using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics;

namespace FirstMVCApp.Controllers
{
    //[Authorize]
    [LogActionFilter()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() // action name
        {
            var model = new HomeViewModel { IsRegistered = false };
            return View(viewName: "Index",model); //action name and view file name should be the same
        }


        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {
            if(string.IsNullOrEmpty(model.SelectedCountry) || string.IsNullOrEmpty(model.FirstName))
            {
                model.IsRegistered = false;
            }
            else
            {
                model.IsRegistered = true;
            }
            return View(viewName: "Index", model);
        }
        public IActionResult Privacy([FromServices] IRepository<Product,int> repository)
        {
            var data = repository.GetAll();
            return Json(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Method Expression Syntax - Used for writing single line methods
        [ResponseCache(Duration =0, Location =ResponseCacheLocation.Any)]
        public string DateAndTime() => DateTime.Now.ToString();
        //Any public method is an Action (It can be called by client) (By default: GET)

        //Url: (GET): /home/NextDate/5
        public string NextDate(int id) => DateTime.Now.AddDays(id).ToString();

        // /home/NextMonth/?month=20   --> QueryString : ?
        public string NextMonth(int month) => DateTime.Now.AddMonths(month).ToString();

        public IActionResult GetJSON()
        {
            var obj = new { Id = 1234, Name = "Sample" };
            return Json(obj);
        }
        public TestClass GetObj()
        {
            return new TestClass() { Id = 9999, TestName = "Sample Test" };
        }
    }

    public class TestClass
    {
        public int Id { get; set; }
        public string TestName { get; set; } = "";
    }
}
