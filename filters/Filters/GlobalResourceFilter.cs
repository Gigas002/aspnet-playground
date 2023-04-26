using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcApp.Filters;

public class GlobalResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("GlobalResourceFilter.OnResourceExecuting");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine("GlobalResourceFilter.OnResourceExecuted");
    }
}
