using Microsoft.AspNetCore.Mvc;
 
namespace MvcApp.Controllers
{
    #region Basic view

    // public class HomeController : Controller
    // {
    //     // public IActionResult Index()
    //     // {
    //     //     ViewData["Message"] = "Hello METANIT.COM";
    //     //     return View();
    //     // }

    //     // public IActionResult Index()
    //     // {
    //     //     ViewBag.People = new List<string> { "Tom", "Sam", "Bob" };
    //     //     return View();
    //     // }

    //     public IActionResult Index()
    //     {
    //         var people = new List<string> { "Tom", "Sam", "Bob" };
    //         return View(people);
    //     }
    // }

    #endregion

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        // public string Index(string username, string password, int age, string comment)
        // {
        //     return $"User Name: {username}   Password: {password}   Age: {age}  Comment: {comment}";
        // }
        // public string Index(bool isMarried) => $"isMarried: {isMarried}";
        // public string Index(string color) => $"color: {color}";
        public string Index(string[] languages)
        {
            string result = "Вы выбрали:";
            foreach (string lang in languages)
            {
                result = $"{result} {lang};";
            }
            return result;
        }


        public IActionResult About() => View();

        public IActionResult Hello()
        {
            return PartialView();
        }
    }
}