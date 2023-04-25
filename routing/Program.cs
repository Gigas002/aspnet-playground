using System.Text;

var builder = WebApplication.CreateBuilder(args);

// проецируем класс SecretCodeConstraint на inline-ограничение secretcode
// builder.Services.Configure<RouteOptions>(options =>
//                 options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));

// альтернативное добавление класса ограничения
// builder.Services.AddRouting(options => options.ConstraintMap.Add("secretcode", typeof(SecretConstraint)));

// builder.Services.AddRouting(options =>
//                 options.ConstraintMap.Add("invalidnames", typeof(InvalidNamesConstraint)));

// builder.Services.AddTransient<TimeService>();   // Добавляем сервис

var app = builder.Build();

// app.Map("/", () => "Index Page");
// app.Map("/about", () => "About Page");
// app.Map("/contact", () => "Contacts Page");
 
// app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
// {
//     var sb = new StringBuilder();
//     var endpoints = endpointSources.SelectMany(es => es.Endpoints);
//     foreach (var endpoint in endpoints)
//     {
//         sb.AppendLine(endpoint.DisplayName);
 
//         // получим конечную точку как RouteEndpoint
//         if (endpoint is RouteEndpoint routeEndpoint)
//         { 
//             sb.AppendLine(routeEndpoint.RoutePattern.RawText);
//         }
 
//         // получение метаданных
//         // данные маршрутизации
//         // var routeNameMetadata = endpoint.Metadata.OfType<Microsoft.AspNetCore.Routing.RouteNameMetadata>().FirstOrDefault();
//         // var routeName = routeNameMetadata?.RouteName;
//         // данные http - поддерживаемые типы запросов
//         //var httpMethodsMetadata = endpoint.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault();
//         //var httpMethods = httpMethodsMetadata?.HttpMethods; // [GET, POST, ...]
//     }
//     return sb.ToString();
// });

// app.Map("/users/{id}/{name}", HandleRequest);
// app.Map("/users/{id?}", (string? id) => $"User Id: {id??"Undefined"}");

// app.Map(
//     "{controller=Home}/{action=Index}/{id?}", 
//     (string controller, string action, string? id) =>
//         $"Controller: {controller} \nAction: {action} \nId: {id}"
// );

// app.Map("users/{**info}", (string info) =>$"User Info: {info}");

// app.Map("/users/{id:int}", (int id) => $"User Id: {id}");

// app.Map(
//     "/users/{name:alpha:minlength(2)}/{age:int:range(1, 110)}",
//     (string name, int age) => $"User Age: {age} \nUser Name:{name}"
// );
// app.Map(
//     "/phonebook/{phone:regex(^7-\\d{{3}}-\\d{{3}}-\\d{{4}}$)}/",
//     (string phone) => $"Phone: {phone}"
// );

// app.Map(
//     "/users/{name}/{token:secretcode(123466)}/",
//     (string name, int token) => $"Name: {name} \nToken: {token}"
// );

// app.Map("/users/{name:invalidnames}", (string name) => $"Name: {name}");

// app.Map("/time", (TimeService timeService) => $"Time: {timeService.Time}");

// app.Map("/hello", () => "Hello METANIT.COM");
// app.Map("/{message}", (string message) => $"Message: {message}");

// app.Map("/{controller}/Index/5", (string controller) => $"Controller: {controller}");
// app.Map("/Home/{action}/{id}", (string action) => $"Action: {action}");

// app.Map("/", () => "Index Page");

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("First middleware starts");
//     await next.Invoke();
//     Console.WriteLine("First middleware ends");
// });
// app.Map("/", () =>
// {
//     Console.WriteLine("Index endpoint starts and ends");
//     return "Index Page";
// });
// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Second middleware starts");
//     await next.Invoke();
//     Console.WriteLine("Second middleware ends");
// });
// app.Map("/about", () =>
// {
//     Console.WriteLine("About endpoint starts and ends");
//     return "About Page";
// });

app.Use(async (context, next) =>
{
    await next.Invoke();
 
    if (context.Response.StatusCode == 404)
        await context.Response.WriteAsync("Resource Not Found");
});
 
app.Map("/", () => "Index Page");
app.Map("/about", () => "About Page");

app.Run();

string HandleRequest(string id, string name)
{
    return $"User Id: {id}   User Name: {name}";
}

public class SecretCodeConstraint : IRouteConstraint
{
    string secretCode; // допустимый код
    public SecretCodeConstraint(string secretCode)
    {
        this.secretCode = secretCode;
    }
 
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return values[routeKey]?.ToString() == secretCode;
    }
}

public class InvalidNamesConstraint : IRouteConstraint
{
    string[] names = new[] { "Tom", "Sam", "Bob" };
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        return !names.Contains(values[routeKey]?.ToString());
    }
}

public class TimeService
{
    public string Time  =>  DateTime.Now.ToLongTimeString();
}
