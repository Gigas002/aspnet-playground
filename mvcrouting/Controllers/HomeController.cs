using Microsoft.AspNetCore.Mvc;
 
// namespace MvcApp.Controllers
// {
//     [Route("Home")]
//     public class HomeController : Controller
//     {
//         [Route("")]
//         [Route("Index/{id:int?}")]
//         public string Index(int? id)
//         {
//             if(id is not null) return $"Product Id: {id}";
             
//             return "Product List";
//         }

//         [Route("About")]
//         public string About(string name, int age) => $"About Page. Name: {name}  Age: {age}";
//     }
// }

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public string Index() => "HomeController вне области";
    }
}