using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace MvcApp.Filters;

public class IEAsyncFilterAttribute : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        // получаем информацию о браузере пользователя
        string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
        if (Regex.IsMatch(userAgent, "MSIE|Trident"))
        {
            context.Result = new ContentResult { Content = "Ваш браузер устарел" };
        }
        else    // если браузер не IE, передаем обработку запроса дальше
            await next();
    }
}
