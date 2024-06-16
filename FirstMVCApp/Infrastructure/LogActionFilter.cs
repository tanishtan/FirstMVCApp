using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace FirstMVCApp.Infrastructure
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        /*private ILogger<LogActionFilterAttribute> _logger;
        public LogActionFilterAttribute(ILogger<LogActionFilterAttribute> logger)
        {
            _logger = logger;
        }*/
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log(nameof(OnActionExecuting), context.RouteData);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log(nameof(OnActionExecuted), context.RouteData);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Log(nameof(OnResultExecuting), context.RouteData);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Log(nameof(OnResultExecuted), context.RouteData);
        }
        private void Log(string methodName, RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = $"{methodName} controller: {controller}, action: {actionName}";
            //_logger.LogInformation(message);
            Debug.WriteLine(message);
        }
    }
}
