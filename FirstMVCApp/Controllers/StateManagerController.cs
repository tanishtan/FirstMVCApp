using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

namespace FirstMVCApp.Controllers
{
    public class StateManagerController : Controller
    {
        //model is a type of view data to be passed
        public IActionResult AboutUs(int id=0)
        {
            ViewData["Message"] = "Message from about us";
            ViewBag.SecondMessage = "Next message from about us";
            TempData["Message"] = "Message from TempData";
            HttpContext.Session.SetString("Message", "Message from session");
            if(id==0)
                return View();
            else
            {
                return RedirectToAction(nameof(ContactUs));
            }
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
