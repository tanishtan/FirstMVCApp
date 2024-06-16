using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMVCApp.Infrastructure
{
    public class SampleActionFilterAttribute : Attribute, IActionFilter //IAsyncActionFilter
    {
        //Before action is executed
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //actionexecutingcontext properties
            //-> actionarguments - enables reading the input 
            //->controller - manipulate the controller
            //result - setting this changes the normal behaviour

            if (context.RouteData.Values["id"] != null)
            {
                var id = Convert.ToInt32(context.ActionArguments["id"]);
                if (id % 2 != 0)
                {
                    //Short circuit the result
                    context.Result = new ViewResult()
                    {
                        ViewName = "NotFound"
                    };
                    
                }
            }
        }

        //after action is executed
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.Canceled = true;
        }
    }

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}


