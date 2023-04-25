using Microsoft.AspNetCore.Mvc;
 
namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    public class HomeController : Controller
    {
        // http://localhost:xxxx/Account, http://localhost:xxxx/Account/Home и http://localhost:xxxx/Account/Home/Index
        [Route("{area}")]
        [Route("{area}/{controller}")]
        [Route("{area}/{controller}/{action}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

public record class Person(int Id, string Name, int Age) : IPerson;

public interface IPerson
{
    public int Id {get;}
    public string Name {get;}
    public int Age {get;}
}