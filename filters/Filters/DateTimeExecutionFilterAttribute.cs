using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcApp.Filters;

public class DateTimeExecutionFilterAttribute : Attribute, IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("DateTime", DateTime.Now.ToString());
    }
    public void OnResultExecuted(ResultExecutedContext _) { }
}
