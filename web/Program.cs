using Microsoft.Extensions.FileProviders;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// create simple middleware service

// app.UseWelcomePage();
// app.MapGet("/", () => "Hello World!");

// app.Run(async (context) =>
// {
//     await context.Response.WriteAsync("Hello METANIT.COM", System.Text.Encoding.Default);
// });

// demonstrate, that once created lives always

// int x = 2;
// app.Run(async (context) =>
// {
//     x = x * 2;  //  2 * 2 = 4
//     await context.Response.WriteAsync($"Result: {x}");
// });

// control response values

// app.Run(async (context) =>
// {
//     var response = context.Response;
//     response.Headers.ContentLanguage = "ru-RU";
//     response.Headers.ContentType = "text/plain; charset=utf-8";
//     //or
//     // response.ContentType = "text/plain; charset=utf-8";
//     response.Headers.Append("secret-id", "256");    // добавление кастомного заголовка
//     await response.WriteAsync("Привет METANIT.COM");
// });

// app.Run(async(context) =>
// {
//     context.Response.StatusCode = 404;
//     await context.Response.WriteAsync("Resource Not Found");
// });

// app.Run(async (context) =>
// {
//     var response = context.Response;
//     response.ContentType = "text/html; charset=utf-8";
//     await response.WriteAsync("<h2>Hello METANIT.COM</h2><h3>Welcome to ASP.NET Core</h3>");
// });

// app.Run(async(context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
//     var stringBuilder = new System.Text.StringBuilder("<table>");
     
//     foreach(var header in context.Request.Headers)
//     {
//         stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
//     }
//     stringBuilder.Append("</table>");
//     await context.Response.WriteAsync(stringBuilder.ToString());
// });

// app.Run(async(context) =>
// {
//     var acceptHeaderValue = context.Request.Headers.Accept;
//     // or
//     // var acceptHeaderValue = context.Request.Headers["accept"];
//     await context.Response.WriteAsync($"Accept: {acceptHeaderValue}");
// });

// read response path

// app.Run(async(context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));

// app.Run(async(context) => 
// {
//     var path = context.Request.Path;
//     var now = DateTime.Now;
//     var response = context.Response;
 
//     if (path=="/date")
//         await response.WriteAsync($"Date: {now.ToShortDateString()}");
//     else if (path == "/time")
//         await response.WriteAsync($"Time: {now.ToShortTimeString()}");
//     else
//         await response.WriteAsync("Hello METANIT.COM");
// });

// app.Run(async(context) => 
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
//     var stringBuilder = new System.Text.StringBuilder("<h3>Параметры строки запроса</h3><table>");
//     stringBuilder.Append("<tr><td>Параметр</td><td>Значение</td></tr>");
//     foreach (var param in context.Request.Query)
//     {
//         stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
//     }
//     stringBuilder.Append("</table>");
//     await context.Response.WriteAsync(stringBuilder.ToString());
// });

// app.Run(async(context) =>
// {
//     string name = context.Request.Query["name"];
//     string age = context.Request.Query["age"];
//     await context.Response.WriteAsync($"{name} - {age}");
// });

// send files

// app.Run(async (context) => await context.Response.SendFileAsync("forest.jpg"));

// app.Run(async(context) => 
// {
//     var path = context.Request.Path;
//     var fullPath = $"html/{path}";
//     var response = context.Response;
 
//     response.ContentType = "text/html; charset=utf-8";
//     if (File.Exists(fullPath))
//     {
//         await response.SendFileAsync(fullPath);
//     }
//     else
//     {
//         response.StatusCode = 404;
//         await response.WriteAsync("<h2>Not Found</h2>");
//     }
// });

// app.Run(async (context) =>
// {
//     context.Response.Headers.ContentDisposition = "attachment; filename=my_forest.jpg";
//     await context.Response.SendFileAsync("forest.jpg");
// });

// app.Run(async (context) =>
// {
//     var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
//     var fileinfo = fileProvider.GetFileInfo("forest.jpg");
 
//     context.Response.Headers.ContentDisposition = "attachment; filename=my_forest2.jpg";
//     await context.Response.SendFileAsync(fileinfo);
// });

// read response data
 
// app.Run(async (context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
 
//     // если обращение идет по адресу "/postuser", получаем данные формы
//     if (context.Request.Path == "/postuser")
//     {
//         var form = context.Request.Form;
//         string name = form["name"];
//         string age = form["age"];
//         await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
//     }
//     else
//     {
//         await context.Response.SendFileAsync("html/index.html");
//     }
// });

// app.Run(async (context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
 
//     // если обращение идет по адресу "/postuser", получаем данные формы
//     if (context.Request.Path == "/postuser")
//     {
//         var form = context.Request.Form;
//         string name = form["name"];
//         string age = form["age"];
//         string[] languages = form["languages"];
//         // создаем из массива languages одну строку
//         string langList = "";
//         foreach (var lang in languages)
//         {
//             langList += $" {lang}";
//         }
//         await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
//             $"<p>Age: {age}</p>" +
//             $"<div>Languages:{langList}</div></div>");
//     }
//     else
//     {
//         await context.Response.SendFileAsync("html/index.html");
//     }
// });

// redirects

// app.Run(async (context) =>
// {
//     if (context.Request.Path == "/old")
//     {
//         context.Response.Redirect("/new");
//     }
//     else if (context.Request.Path == "/new")
//     {
//         await context.Response.WriteAsync("New Page");
//     }
//     else
//     {
//         await context.Response.WriteAsync("Main Page");
//     }
// });

// json

// app.Run(async (context) =>
// {
//     Person tom = new("Tom", 22);
//     await context.Response.WriteAsJsonAsync(tom);
// });

// app.Run(async (context) =>
// {
//     var response = context.Response;
//     response.Headers.ContentType = "application/json; charset=utf-8";
//     await response.WriteAsync("{'name':'Tom', 'age':37}");
// });

// app.Run(async (context) =>
// {
//     var response = context.Response;
//     var request = context.Request;
//     if (request.Path == "/api/user")
//     {
//         var message = "Некорректные данные";   // содержание сообщения по умолчанию
//         try
//         {
//             // пытаемся получить данные json
//             var person = await request.ReadFromJsonAsync<Person>();
//             if (person != null) // если данные сконвертированы в Person
//                 message = $"Name: {person.Name}  Age: {person.Age}";
//         }
//         catch { }
//         // отправляем пользователю данные
//         await response.WriteAsJsonAsync(new { text = message });
//     }
//     else
//     {
//         response.ContentType = "text/html; charset=utf-8";
//         await response.SendFileAsync("html/index.html");
//     }
// });

// app.Run(async (context) =>
// {
//     var response = context.Response;
//     var request = context.Request;
//     if (request.Path == "/api/user")
//     {
//         var responseText = "Некорректные данные";   // содержание сообщения по умолчанию
 
//         if (request.HasJsonContentType())
//         {
//             // определяем параметры сериализации/десериализации
//             var jsonoptions = new JsonSerializerOptions();
//             // добавляем конвертер кода json в объект типа Person
//             jsonoptions.Converters.Add(new PersonConverter());
//             // десериализуем данные с помощью конвертера PersonConverter
//             var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
//             if (person != null)
//                 responseText = $"Name: {person.Name}  Age: {person.Age}";
//         }
//         await response.WriteAsJsonAsync(new {text = responseText});
//     }
//     else
//     {
//         response.ContentType = "text/html; charset=utf-8";
//         await response.SendFileAsync("html/index.html");
//     }
// });

// upload files to server

// app.Run(async (context) =>
// {
//     var response = context.Response;
//     var request = context.Request;
 
//     response.ContentType = "text/html; charset=utf-8";
 
//     if (request.Path == "/upload" && request.Method=="POST")
//     {
//         IFormFileCollection files = request.Form.Files;
//         // путь к папке, где будут храниться файлы
//         var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
//         // создаем папку для хранения файлов
//         Directory.CreateDirectory(uploadPath);
 
//         foreach (var file in files)
//         {
//             // путь к папке uploads
//             string fullPath = $"{uploadPath}/{file.FileName}";
            
//             // сохраняем файл в папку uploads
//             using (var fileStream = new FileStream(fullPath, FileMode.Create))
//             {
//                 await file.CopyToAsync(fileStream);
//             }
//         }
//         await response.WriteAsync("Файлы успешно загружены");
//     }
//     else
//     {
//         await response.SendFileAsync("html/index.html");
//     }
// });

// use

// app.Use(GetDate);
// app.Run(async (context) => await context.Response.WriteAsync("Hello METANIT.COM"));

// app.UseWhen(
//     context => context.Request.Path == "/time", // условие: если путь запроса "/time"
//     HandleTimeRequest
// );
 
// app.Run(async context =>
// {
//     await context.Response.WriteAsync("Hello METANIT.COM");
// });

// map

// app.Map("/time", appBuilder =>
// {
//     var time = DateTime.Now.ToShortTimeString();
 
//     // логгируем данные - выводим на консоль приложения
//     appBuilder.Use(async(context, next) =>
//     {
//         Console.WriteLine($"Time: {time}");
//         await next();   // вызываем следующий middleware
//     });
 
//     appBuilder.Run(async context => await context.Response.WriteAsync($"Time: {time}"));
// });
 
// app.Run(async (context) => await context.Response.WriteAsync("Hello METANIT.COM"));

// app.Map("/index", Index);
// app.Map("/about", About);

// app.Map("/home", appBuilder =>
// {
//     appBuilder.Map("/index", Index); // middleware для "/home/index"
//     appBuilder.Map("/about", About); // middleware для "/home/about"
//     // middleware для "/home"
//     appBuilder.Run(async (context) => await context.Response.WriteAsync("Home Page"));
// });
 
// app.Run(async (context) => await context.Response.WriteAsync("Page Not Found"));

// create and use middleware

// app.UseMiddleware<TokenMiddleware>();
 
// app.UseToken("55");

// app.Run(async(context) => await context.Response.WriteAsync("Hello METANIT.COM"));

// middleware ordering

// app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseMiddleware<AuthenticationMiddleware>();
// app.UseMiddleware<RoutingMiddleware>();

// environment info (see launchSettings.json)

// app.Environment.EnvironmentName = "Production";
// if (app.Environment.IsDevelopment())
// {
//     app.Run(async (context) => await context.Response.WriteAsync("In Development Stage"));
// }
// else
// {
//     app.Run(async (context) => await context.Response.WriteAsync("In Production Stage"));
// }
// Console.WriteLine($"{app.Environment.EnvironmentName}");

app.Environment.EnvironmentName = "Test";   // изменяем название среды на Test
 
if (app.Environment.IsEnvironment("Test")) // Если проект в состоянии "Test"
{
    app.Run(async (context) => await context.Response.WriteAsync("In Test Stage"));
}
else
{
    app.Run(async (context) => await context.Response.WriteAsync("In Development or Production Stage"));
}

app.Run();

void Index(IApplicationBuilder appBuilder)
{
    appBuilder.Run(async context => await context.Response.WriteAsync("Index"));
}
void About(IApplicationBuilder appBuilder)
{
    appBuilder.Run(async context => await context.Response.WriteAsync("About"));
}

void HandleTimeRequest(IApplicationBuilder appBuilder)
{
    appBuilder.Use(async (context, next) =>
    {
        var time = DateTime.Now.ToShortTimeString();
        Console.WriteLine($"current time: {time}");
        await next();   // вызываем следующий middleware
    });
}

// async Task GetDate(HttpContext context, Func<Task> next)
// {
//     string? path = context.Request.Path.Value?.ToLower();
//     if (path == "/date")
//     {
//         await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//     }
//     else
//     {
//         await next.Invoke();
//     }
// }

async Task GetDate(HttpContext context, RequestDelegate next)
{
    string? path = context.Request.Path.Value?.ToLower();
    if (path == "/date")
    {
        await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
    }
    else
    {
        await next.Invoke(context);
    }
}

public record Person(string Name, int Age);

public class PersonConverter : JsonConverter<Person>
{
    public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var personName = "Undefined";
        var personAge = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                switch (propertyName?.ToLower())
                {
                    // если свойство age и оно содержит число
                    case "age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();  // считываем число из json
                        break;
                    // если свойство age и оно содержит строку
                    case "age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // пытаемся конвертировать строку в число
                        if (int.TryParse(stringValue, out int value))
                        {
                            personAge = value;
                        }
                        break;
                    case "name":    // если свойство Name/name
                        string? name = reader.GetString();
                        if(name!=null)
                            personName = name;
                        break;
                }
            }
        }
        return new Person(personName, personAge);
    }
    // сериализуем объект Person в json
    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);
 
        writer.WriteEndObject();
    }
}

public class TokenMiddleware
{
    private readonly RequestDelegate next;
    string pattern;
    public TokenMiddleware(RequestDelegate next, string pattern)
    {
        this.next = next;
        this.pattern = pattern;
    }
 
    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["token"];
        if (token != pattern)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
        }
        else
        {
            await next.Invoke(context);
        }
    }
}

public static class TokenExtensions
{
    public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
    {
        return builder.UseMiddleware<TokenMiddleware>(pattern);
    }
}

public class RoutingMiddleware
{
    public RoutingMiddleware(RequestDelegate _)
    {
    }
    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;
        if (path == "/index")
        {
            await context.Response.WriteAsync("Home Page");
        }
        else if (path == "/about")
        {
            await context.Response.WriteAsync("About Page");
        }
        else
        {
            context.Response.StatusCode = 404;
        }
    }
}

public class AuthenticationMiddleware
{
    readonly RequestDelegate next;
    public AuthenticationMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["token"];
        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = 403;
        }
        else
        {
            await next.Invoke(context);
        }
    }
}

public class ErrorHandlingMiddleware
{
    readonly RequestDelegate next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        await next.Invoke(context);
        if (context.Response.StatusCode == 403)
        {
            await context.Response.WriteAsync("Access Denied");
        }
        else if (context.Response.StatusCode == 404)
        {
            await context.Response.WriteAsync("Not Found");
        }
    }
}
