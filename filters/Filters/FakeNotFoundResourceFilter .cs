using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcApp.Filters;

public class FakeNotFoundResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuted(ResourceExecutedContext context)
    {

    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.Result = new ContentResult { Content = "Ресурс не найден" };
    }
}
