using Microsoft.AspNetCore.Mvc;
using FirstMVCApp.Models;
using FirstMVCApp.Infrastructure;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace FirstMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product, int> _repository;
        /*public ProductsController()
        {
            //_repository = new ProductListRepository();
            _repository = new ProductEFRepository();
        }*/
        public ProductsController(IRepository<Product, int> repository) => _repository = repository;//This is for Dependency Injection

        //Get: /products/list
        public IActionResult List()
        {
            var items = _repository.GetAll();
            ViewData["Title"] = "Product Management";
            return View(items);
        }

        [SampleActionFilter]
        public IActionResult Details(int id) 
        {
            var model = _repository.GetById(id);
            if(model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        // Routing Table: Controller: Products, Action: AddNew, HttpMethod: Get, Class=ProductsController, Method: CreateNewProduct
        [HttpGet] // defualt is HTTPGET itself
        [ActionName("Addnew")]
        [Authorize]
        public IActionResult CreateNewProduct() //u can change the method name as well
        {
            var model = new Product();
            return View(viewName: "CreateNew", model: model);
        }

        [HttpPost]//Defualt, action name is method name, http method = get -> changed to POST
        [Authorize]
        public IActionResult CreateNew(IFormCollection collection)//do not write the function parameter if you want to use the commented thing
        {
            /*var id = Convert.ToInt32(Request.Form["Productid"]); //request.form binder is used
            var name = Request.Form["ProductName"];
            var price = Convert.ToDecimal(Request.Form["UnitPrice"]);
            var stock = Convert.ToInt16(Request.Form["UnitsInStock"]);*/
            var id = Convert.ToInt32(collection["ProductId"]);
            var name = Convert.ToString(collection["ProductName"]);
            var price = Convert.ToDecimal(collection["UnitPrice"]);
            var stock = Convert.ToInt16(collection["UnitsInStock"]);
            
            var product = new Product()
            {
                ProductId = id,
                ProductName = name,
                UnitPrice = price,
                UnitsInStock = stock
            };

            //validation part for the model
            /*bool isValid = true;
            if (product.UnitPrice < 1)
            {
                ModelState.AddModelError("UnitPrice", "Price cannot be 0");
                isValid = false;
            }
            if(product.UnitsInStock<1)
            {
                ModelState.AddModelError("UnitsInStock", "Stock cannot be 0");
                isValid = false;
            }
            if (string.IsNullOrEmpty(product.ProductName) || product.ProductName.Length < 4)
            {
                ModelState.AddModelError("ProductName", "Product name is required and should be atleast 3 or more char");
                isValid = false;
            }
            if(!isValid)
            {
                return View(product);
            }*/
            //End of validation
            //this.TryValidateModel(product);
            if(!TryValidateModel(product))
                return View(product);

            //ToDo: Complete the repository code before executing the methods
            _repository.CreateNew(product);
            return RedirectToAction(nameof(List));
        }

        [ValidateModel]
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var model = _repository.GetById(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, Product model)
        {
            /*if(!ModelState.IsValid)
            {
                return View(model);
            }*/
            _repository.Update(model);
            return RedirectToAction(nameof(List));
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            var model = _repository.GetById(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [ActionName("Remove")]
        [HttpPost]
        [Authorize]
        public IActionResult RemoveConfirmed(int productId)
        {
            _repository.Remove(productId);
            return RedirectToAction(nameof(List));
        }

        // GET: /products/index
        //strongly typed vies
        public IActionResult Index()
        {
            var item = new Product()
            {
                ProductId = 1,
                ProductName = "Sample",
                UnitPrice = 1234,
                UnitsInStock = 1234
            };
            //Dictonary object : Weakly typed view
            // The viewdata is what is sent to the view from the controller.
            ViewData["Title"] = "Product Management";

            //stronly typed
            return View(model: item);
        }
        
    }
}
