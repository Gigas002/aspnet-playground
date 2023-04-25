using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : LogBaseController
    {
        #region Передача данных через строку запроса

        // public async Task Index() 
        // {
        //     Response.ContentType = "text/html;charset=utf-8";
        //     await Response.WriteAsync("<h2>Hello METANIT.COM</h2>");
        // }

        // public async Task Index() 
        // {
        //     Response.ContentType = "text/html;charset=utf-8";
        //     System.Text.StringBuilder tableBuilder = new("<h2>Request headers</h2><table>");
        //     foreach (var header in Request.Headers)
        //     {
        //         tableBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        //     }
        //     tableBuilder.Append("</table>");
        //     await Response.WriteAsync(tableBuilder.ToString());
        // }

        // Home/Index?name=Tom&age=37
        // public string Index(string name = "Bob", int age = 33)
        // {
        //     return $"Name: {name}  Age: {age}";
        // }

        // Home/Index?name=Tom&age=37
        // public string Index(Person person)
        // {
        //     return $"Person Name: {person.Name}  Person Age: {person.Age}";
        // }

        // public string Index()
        // {
        //     string name = Request.Query["name"];
        //     string age = Request.Query["age"];
        //     return $"Name: {name}  Age: {age}";
        // }

        // Home/Index?people=Tom&people=Bob&people=Sam
        // public string Index(string[] people)
        // {
        //     string result = "";
        //     foreach (var person in people)
        //         result = $"{result}{person}; ";
        //     return result;
        // }

        // Home/Index?people[0].name=Tom&people[0].age=37&people[1].name=Bob&people[1].age=41
        // public string Index(Person[] people)
        // {
        //     string result = "";
        //     foreach (Person person in people)
        //     {
        //         result = $"{result} {person.Name}; ";
        //     }
        //     return result ;
        // }

        // Home/Index?items[germany]=berlin&items[france]=paris&items[spain]=madrid
        // public string Index (Dictionary<string,string> items)
        // {
        //     string result = "";
        //     foreach (var item in items)
        //     {
        //         result = $"{result} {item.Key} - {item.Value}; ";
        //     }
        //     return result ;
        // }

        #endregion

        #region Передача данных через формы (GET/POST)

        // [HttpGet]
        // public async Task Index()
        // {
        //     string content = @"<form method='post'>
        //         <label>Name:</label><br />
        //         <input name='name' /><br />
        //         <label>Age:</label><br />
        //         <input type='number' name='age' /><br />
        //         <input type='submit' value='Send' />
        //     </form>";
        //     Response.ContentType = "text/html;charset=utf-8";
        //     await Response.WriteAsync(content);
        // }

        // [HttpPost]
        // // public string Index(string name, int age) => $"{name}: {age}";
        // public string Index(Person person) => $"{person.Name}: {person.Age}";

        // public async Task Index()
        // {
        //     string form = @"<form method='post'>
        //         <p><input name='names' /></p>
        //         <p><input name='names' /></p>
        //         <p><input name='names' /></p>
        //         <input type='submit' value='Send' />
        //     </form>";
        //     Response.ContentType = "text/html;charset=utf-8";
        //     await Response.WriteAsync(form);
        // }
        // [HttpPost]
        // public string Index(string[] names)
        // {
        //     string result = "";
        //     foreach(string name in names)
        //     {
        //         result = $"{result} {name}";
        //     }
        //     return result;
        // }

        //     [HttpGet]
        //     public async Task Index()
        //     {
        //         string content = @"<form method='post'>
        //     <p>
        //         Германия:
        //         <input type='text' name='items[germany]' />
        //     </p>
        //     <p>
        //         Франция:
        //         <input type='text' name='items[france]' />
        //     </p>
        //     <p>
        //         Испания:
        //         <input type='text' name='items[spain]' />
        //     </p>
        //     <p>
        //         <input type='submit' value='Отправить' />
        //     </p>
        // </form>";
        //         Response.ContentType = "text/html;charset=utf-8";
        //         await Response.WriteAsync(content);
        //     }
        //     [HttpPost]
        //     public string Index(Dictionary<string, string> items)
        //     {
        //         string result = "";
        //         foreach (var item in items)
        //         {
        //             result = $"{result} {item.Key} - {item.Value}; ";
        //         }
        //         return result;
        //     }

        // [HttpGet]
        // public async Task Index()
        // {
        //     string form = @"<form method='post'>
        //         <p>
        //             Person1 Name:<br/> 
        //             <input name='people[0].name' /><br/>
        //             Person1 Age:<br/>
        //             <input name='people[0].age' />
        //         </p>
        //         <p>
        //             Person2 Name:<br/> 
        //             <input name='people[1].name' /><br/>
        //             Person2 Age:<br/>
        //             <input name='people[1].age' />
        //         </p>
        //         <input type='submit' value='Send' />
        //     </form>";
        //     Response.ContentType = "text/html;charset=utf-8";
        //     await Response.WriteAsync(form);
        // }

        // [HttpPost]
        // public string Index(Person[] people)
        // {
        //     string result = "";
        //     foreach (Person person in people)
        //     {
        //         result = $"{result} \n{person}";
        //     }
        //     return result;
        // }

        #endregion

        #region Results

        // public IActionResult Index()
        // {
        //     return new HtmlResult("<h2>Hello METANIT.COM!</h2>");
        // }

        /*
        public string Index()
        {
            return "Hello METANIT.COM";
        }
        */
        // the same as:
        // public IActionResult Index()
        // {
        //     return Content("Hello METANIT.COM");
        // }

        // public IActionResult Index()
        // {
        //     var tom = new Person("Tom", 37);
        //     var jsonOptions = new System.Text.Json.JsonSerializerOptions 
        //     { 
        //         PropertyNameCaseInsensitive = true, // учитываем регистр
        //         WriteIndented = true                // отступы для красоты
        //     };
        //     return Json(tom, jsonOptions);
        // }

        // public IActionResult Index() => Content("Index");
        // public IActionResult About() => Content("About");
    
        // public IActionResult Contact()
        // {
        //     // return Redirect("~/Home/About");
        //     return RedirectPermanent("~/Home/About");
        // }

        // public IActionResult Index()
        // {
        //     return RedirectToAction("About", "Home", new { name = "Tom", age = 37 });
        // }
    
        // public IActionResult About(string name, int age) => Content($"Name:{name}  Age: {age}");

        // public IActionResult Index()
        // {
        //     return StatusCode(401);
        // }

        // public IActionResult Index()
        // {
        //     return NotFound("Resource not found");
        // }

        // public IActionResult Index(int age)
        // {
        //     if (age < 18) return Unauthorized(new Error("Access is denied"));
        //     return Content("Access is available");
        // }

        #endregion

        #region Send files

        // public IActionResult GetFile()
        // {
        //     // Путь к файлу
        //     string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/hello.txt");
        //     // Тип файла - content-type
        //     string file_type = "text/plain";
        //     // Имя файла - необязательно
        //     string file_name = "hello.txt";
        //     return PhysicalFile(file_path, file_type, file_name);
        // }

        // // Отправка массива байтов
        // public IActionResult GetBytes()
        // {
        //     string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/hello.txt");
        //     byte[] mas = System.IO.File.ReadAllBytes(path);
        //     string file_type = "text/plain";
        //     string file_name = "hello2.txt";
        //     return File(mas, file_type, file_name);
        // }

        // // Отправка потока
        // public IActionResult GetStream()
        // {
        //     string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/hello.txt");
        //     FileStream fs = new FileStream(path, FileMode.Open);
        //     string file_type = "text/plain";
        //     string file_name = "hello3.txt";
        //     return File(fs, file_type, file_name);
        // }

        // public IActionResult GetVirtualFile() => 
        //     File("Files/hello.txt", "text/plain", "hello4.txt");

        #endregion

        #region DI

        // readonly ITimeService timeService;
 
        // public HomeController(ITimeService timeServ)
        // {
        //     timeService = timeServ;
        // }
        // public string Index() => timeService.Time;

        // public string Index([FromServices] ITimeService timeService)
        // {
        //     return timeService.Time;
        // }

        public string Index()
        {
            ITimeService? timeService = HttpContext.RequestServices.GetService<ITimeService>();
            return timeService?.Time ?? "Undefined";
        }

        #endregion

        protected internal string Hello() => "Hello ASP.NET";
    }
}

public record class Person(string Name, int Age);

record class Error(string Message);
