using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Services;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")] // url: /api/home
    [ApiController]
    [CustomAuthorization]
    public class HomeController : ControllerBase
    {
        // /api/home/datetime
        [HttpGet(template: "datetime")]
        public string ServerDateTime() => DateTime.Now.ToString();

        // /api/home/name
        [HttpGet(template: "name")]
        public string ServerApiName() => typeof(HomeController).FullName!;

        [HttpGet(template: "nextday/{day}")]
        public string NextDay(int day) => $"{DateTime.Now.AddDays(day).ToString()}";

        [HttpGet(template: "futureDate/{month:int}")]
        public string FutureDate(int month) => $"{DateTime.Now.AddMonths(month).ToString()}";

        [HttpGet("testdata")]
        public TestClass GetData()
        {
            return new TestClass { Id = 1, Name = "Sample" };
        }

        [HttpPost]
        public IActionResult Post(TestClass model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            if (model.Name.Contains("a"))
                return Ok(model); // 200
            else
                return NotFound();
        }

        [HttpPut("{key}")] // find and replace
        public IActionResult GetPut(string key, TestClass model)
        {
            if(int.TryParse(key, out int value))
            {
                if(value==model.Id)
                    return Ok(model);   
                else 
                    return NotFound();
            }
            else
            {
                return BadRequest("Invalid key passed");
            }
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<TestClass> GetTestClass()
        {
            return Ok(new TestClass());
        }
    }
    public class TestClass()
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

