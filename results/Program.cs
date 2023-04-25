using System.Text.Json;

// var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions { WebRootPath = "Files" });  // добавляем папку для хранения файлов
var app = builder.Build();

// app.Map("/hello", SendHello);
// app.Map("/", () => "Hello ASP.NET Core");

// app.Run(async context =>
// {
//     await Results.Text("Hello ASP.NET Core").ExecuteAsync(context);
// });

// app.Map("/chinese", () => Results.Text("你好", "text/plain", System.Text.Encoding.Unicode));
// app.Map("/", () => Results.Text("Hello World"));

// app.Map("/person", () => Results.Json(new Person("Bob", 41)));   // отправка объекта Person
// app.Map("/", () => Results.Json(new { name = "Tom", age = 37 })); // отправка анонимного объекта

// app.Map("/sam", () => Results.Json(new Person("Sam", 25),
//         new JsonSerializerOptions()
//         {
//             PropertyNameCaseInsensitive = false,
//             NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString
//         }));
 
// app.Map("/bob", () => Results.Json(new Person("Bob", 41), 
//         new JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web))); 
 
// app.Map("/tom", () => Results.Json(new Person("Tom", 37),
//          new JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.General)));

// app.Map("/error", () => Results.Json(new {message="Unexpected error"}, statusCode: 500));
 
// app.Map("/old", () => Results.LocalRedirect("/new"));
// app.Map("/new", () => "New Address");
// app.Map("/old", () => Results.Redirect("https://metanit.com"));
//  app.Map("/about", () => Results.StatusCode(401));
// app.Map("/about", () => Results.NotFound(new { message= "Resource Not Found"}));
// app.Map("/contacts", () => Results.NotFound("Error 404. Invalid address"));
// app.Map("/contacts", () => Results.Unauthorized());
// app.Map("/contacts/{age:int}", (int age) => 
// {
//     if (age < 18)
//         return Results.BadRequest(new { message = "Invalid age" });
//     else
//         return Results.Content("Access is available");
// });
// app.Map("/about", () => Results.Ok("Laudate omnes gentes laudate"));
// app.Map("/contacts", () => Results.Ok(new { message = "Success!" }));

// app.Map("/forest", async () => 
// {
//     string path = "Files/forest.png";
//     byte[] fileContent = await File.ReadAllBytesAsync(path);  // считываем файл в массив байтов
//     string contentType = "image/png";       // установка mime-типа
//     string downloadName = "winter_forest.png";  // установка загружаемого имени
//     return Results.File(fileContent, contentType, downloadName);
// });

// app.Map("/forest", () => 
// {
//     string path = "Files/forest.png";
//     FileStream fileStream = new FileStream(path, FileMode.Open);
//     string contentType = "image/png";
//     string downloadName = "winter_forest.png";
//     return Results.File(fileStream, contentType, downloadName);
// });

// app.Map("/forest", () => 
// {
//     string path = "Files/forest.png";
//     string contentType = "image/png";
//     string downloadName = "winter_forest.png";
//     return Results.File(path, contentType, downloadName);
// });

// app.Map("/river", () => 
// {
//     string path = "newRiver.jpg";
//     string contentType = "image/jpeg";
//     string downloadName = "river.jpg";
//     return Results.File(path, contentType, downloadName);
// });

// app.Map("/", () => "Hello World");

// отправляем html-код при обращении по пути "/"
app.Map("/", () => Results.Extensions.Html("""
<!DOCTYPE html>
<html>
<head>
<title>METANIT.COM</title>
<meta charset='utf-8' />
</head>
<body>
<h1>Hello METANIT.COM</h1>
</body>
</html>
"""));

app.Run();

IResult SendHello()
{
    return Results.Text("Hello ASP.NET Core");
}

record class Person(string Name, int Age);

class HtmlResult: IResult
{
    string htmlCode = "";
    public HtmlResult(string htmlCode) => this.htmlCode = htmlCode;
 
    public async Task ExecuteAsync(HttpContext context)
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(htmlCode);
    }
}
 
static class ResultsHtmlExtension
{
    public static IResult Html(this IResultExtensions ext, string htmlCode) => new HtmlResult(htmlCode);
}
