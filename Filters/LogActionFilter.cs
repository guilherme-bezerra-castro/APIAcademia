using Microsoft.AspNetCore.Mvc.Filters;

namespace APIAcademia.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;
        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            _logger.LogInformation(
                "[FIM] {Controller}/{Action} | Status: {Status}",
                controller, action, context.HttpContext.Response.StatusCode);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            _logger.LogInformation(
                "[INÍCIO] {Controller}/{Action} | {Method} {Path}",
                controller, action, context.HttpContext.Request.Method, context.HttpContext.Request.Path);
        }
    }
}
