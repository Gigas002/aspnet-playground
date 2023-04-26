using Microsoft.AspNetCore.Mvc;
using MvcApp.Filters;  // пространство имен фильтра SimpleResourceFilter

namespace MvcApp.Controllers;

// // [SimpleResourceFilter]
// public class HomeController : Controller
// {
//     [FakeNotFoundResourceFilter]
//     public IActionResult Index() => View();
// }

[ControllerResourceFilter(int.MinValue)]
public class HomeController : Controller
{
    [ActionResourceFilter]
    [Whitespace]
    [DateTimeExecutionFilter]
    [CustomExceptionFilter]

    // [SimpleResourceFilter(30, "8h6ell3o5wor5ld8")]
    public IActionResult Index()
    {
        int x = 0;
        int y = 8 / x;
        
        return View();
    }
}
