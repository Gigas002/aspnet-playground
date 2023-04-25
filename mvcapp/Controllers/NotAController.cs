using Microsoft.AspNetCore.Mvc;
 
namespace MvcApp.Controllers
{
    [NonController]
    public class NotAController : Controller
    {
        [NonAction]
        public string Index() => "Hello METANIT.COM";

        [ActionName("Welcome")]
        public string Hello() => "Hello ASP.NET";
    }
}