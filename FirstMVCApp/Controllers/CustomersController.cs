using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace FirstMVCApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer, int> _crepository;
        public CustomersController()
        {
            _crepository = new CustomerListRepository();
        }

        //Get: /products/list
        public IActionResult List()
        {
            var items = _crepository.GetAll();
            ViewData["Title"] = "Customer Management";
            return View(items);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var model = _crepository.GetById(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("AddNew")]
        public IActionResult CreateNewCustomer() 
        {
            var model = new Customer();
            return View(viewName: "Create", model: model);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            var id = Convert.ToString(_crepository.GetAll().Count()+1);
            
            var name = Convert.ToString(collection["CompanyName"]);
            var contact = Convert.ToString(collection["ContactName"]);
            var city = Convert.ToString(collection["City"]);
            var country = Convert.ToString(collection["Country"]);

            var customer = new Customer()
            {
                CustomerId = id,
                CompanyName = name,
                ContactName = contact,
                City = city,
                Country = country
            };
            if (!TryValidateModel(customer))
                return View(customer);
            _crepository.CreateNew(customer);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _crepository.GetById(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, Customer model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _crepository.Update(model);
            return RedirectToAction(nameof(List));
        }

        public IActionResult Remove(int id)
        {
            //string sId = id.ToString();
            var model = _crepository.GetById(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [ActionName("Remove")]
        [HttpPost]
        public IActionResult RemoveConfirmed(int customerId)
        {
            _crepository.Remove(customerId);
            return RedirectToAction(nameof(List));
        }


    }
}
