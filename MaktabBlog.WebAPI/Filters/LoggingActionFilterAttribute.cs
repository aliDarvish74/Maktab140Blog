using MaktabBlog.Business.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MaktabBlog.WebAPI.Filters;

public class LoggingActionFilterAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("[Logging action filter] Request came");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("[Logging action filter] Response is ready");
    }
}