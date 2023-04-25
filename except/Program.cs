var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.UseDeveloperExceptionPage();
app.Environment.EnvironmentName = "Production"; // меняем имя окружения

// // обработка ошибок HTTP
// app.UseStatusCodePages("text/plain", "Error: Resource Not Found. Status code: {0}");

// app.UseStatusCodePages(async statusCodeContext =>
// {
//     var response = statusCodeContext.HttpContext.Response;
//     var path = statusCodeContext.HttpContext.Request.Path;
 
//     response.ContentType = "text/plain; charset=UTF-8";
//     if (response.StatusCode == 403)
//     {
//         await response.WriteAsync($"Path: {path}. Access Denied ");
//     }
//     else if (response.StatusCode == 404)
//     {
//         await response.WriteAsync($"Resource {path} Not Found");
//     }
// });

// app.UseStatusCodePagesWithRedirects("/error/{0}");

// app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

// // если приложение не находится в процессе разработки
// // перенаправляем по адресу "/error"
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
// }
 
// // middleware, которое обрабатывает исключение
// app.Map("/error", app => app.Run(async context =>
// {
//     context.Response.StatusCode = 500;
//     await context.Response.WriteAsync("Error 500. DivideByZeroException occurred!");
// }));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(app => app.Run(async context =>
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Error 500. DivideByZeroException occurred!");
    }));
}

// // middleware, где генерируется исключение
// app.Run(async (context) =>
// {
//     int a = 5;
//     int b = 0;
//     int c = a / b;
//     await context.Response.WriteAsync($"c = {c}");
// });

// app.Map("/hello", () => "Hello ASP.NET Core");

app.Map("/hello", () => "Hello ASP.NET Core");
app.Map("/error/{statusCode}", (int statusCode) => $"Error. Status Code: {statusCode}");

app.Run();
