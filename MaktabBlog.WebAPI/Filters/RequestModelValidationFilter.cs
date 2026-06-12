using System.Text.Json;
using MaktabBlog.Business.Abstraction.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MaktabBlog.WebAPI.Filters;

public class RequestModelValidationFilter : IActionFilter
{
    public RequestModelValidationFilter()
    {
        Console.WriteLine("Filter instantiated.");
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            throw new BaseBusinessException(string.Join(",", errors),"ValidationError_400");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}