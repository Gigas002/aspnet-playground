using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApp.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();   // добавляем middleware для работы с сессиями

// app.Use(async (context, next) =>
// {
//     context.Items["text"] = "Hello from HttpContext.Items";
//     await next.Invoke();
// });
 
// app.Run(async (context) => await context.Response.WriteAsync($"Text: {context.Items["text"]}"));

// app.Use(async (context, next) =>
// {
//     context.Items.Add("message", "Hello METANIT.COM");
//     await next.Invoke();
// });
 
// app.Run(async (context) =>
// {
//     if (context.Items.ContainsKey("message"))
//         await context.Response.WriteAsync($"Message: {context.Items["message"]}");
//     else
//         await context.Response.WriteAsync("Random Text");
// });

// cookies

// app.Run(async (context) =>
// {
//     // if (context.Request.Cookies.ContainsKey("name"))
//     // {
//     //     string? name = context.Request.Cookies["name"];
//     //     await context.Response.WriteAsync($"Hello {name}!");
//     // }
//     if (context.Request.Cookies.TryGetValue("name", out var login))
//     {
//         await context.Response.WriteAsync($"Hello {login}!");
//     }
//     else
//     {
//         context.Response.Cookies.Append("name", "Tom");
//         await context.Response.WriteAsync("Hello World!");
//     }
// });

// sessions

// app.Run(async (context) =>
// {
//     if (context.Session.Keys.Contains("name"))
//         await context.Response.WriteAsync($"Hello {context.Session.GetString("name")}!");
//     else
//     {
//         context.Session.SetString("name", "Tom");
//         await context.Response.WriteAsync("Hello World!");
//     }
// });

app.Run(async (context) =>
{
    if (context.Session.Keys.Contains("person"))
    {
        Person? person = context.Session.Get<Person>("person");
        await context.Response.WriteAsync($"Hello {person?.Name}, your age: {person?.Age}!");
    }
    else
    {
        Person person = new Person { Name = "Tom", Age = 22 };
        context.Session.Set<Person>("person", person);
        await context.Response.WriteAsync("Hello World!");
    }
});

app.Run();

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize<T>(value));
    }
 
    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }
}

class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
