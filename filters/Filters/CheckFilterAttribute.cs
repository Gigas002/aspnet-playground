using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcApp.Filters;

public class CheckFilterAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context,
                                                    ActionExecutionDelegate next)
    {
        context.ActionArguments["id"] = 34;
        await next();
    }
}
