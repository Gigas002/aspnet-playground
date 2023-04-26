using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcApp.Filters;

public class ControllerResourceFilter : Attribute, IResourceFilter, IOrderedFilter
{
    public ControllerResourceFilter(int order) => Order = order;
    
    public int Order { get; }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine("ControllerResourceFilter.OnResourceExecuting");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine("ControllerResourceFilter.OnResourceExecuted");
    }
}
