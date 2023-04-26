using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcApp.Filters;

public class ActionResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("ActionResourceFilter.OnResourceExecuting");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine("ActionResourceFilter.OnResourceExecuted");
    }
}
