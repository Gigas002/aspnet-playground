using Microsoft.AspNetCore.Rewrite; // Пакет с middleware URL Rewriting
 
var builder = WebApplication.CreateBuilder();
 
var app = builder.Build();
 
// // подключаем URL Rewriting
// var options = new RewriteOptions()
//                     .AddRedirect("home[/]?$", "home/index"); // переадресация с home на home/index
// app.UseRewriter(options);
 
// app.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
// app.MapGet("/home", async context => await context.Response.WriteAsync("Home Page!"));
// app.MapGet("/home/index", async context => await context.Response.WriteAsync("Home Index Page!"));
 
// // подключаем URL Rewriting
// var options = new RewriteOptions()
//             .AddRedirect("(.*)/$", "$1")
//             .AddRewrite("home/index", "home/about", skipRemainingRules: false);
// app.UseRewriter(options);
 
// app.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
// app.MapGet("/home/about", async context =>
//         await context.Response.WriteAsync($"About: {context.Request.Path}"));
// app.MapGet("/home/index", async context =>
//         await context.Response.WriteAsync("Home Index Page!"));

// // подключаем URL Rewriting
// var options = new RewriteOptions()
//             .AddRewrite(@"product/(\w+)/(\d+)", 
//                 "home/products?cat=$1&id=$2",
//                 skipRemainingRules: false);
// app.UseRewriter(options);
 
// app.MapGet("/", async context =>
// {
//     await context.Response.WriteAsync("Hello World!");
// });
// app.MapGet("/home/products", async context =>
// {
//     await context.Response.WriteAsync($"cat: {context.Request.Query["cat"]} id: {context.Request.Query["id"]}");
// });

IHostEnvironment? env = app.Services.GetService<IHostEnvironment>();
if(env is not null)
{
    var options = new RewriteOptions()
                    .AddIISUrlRewrite(env.ContentRootFileProvider, "urlrewrite.xml");
    app.UseRewriter(options);
}
 
app.MapGet("/", async context => await context.Response.WriteAsync("Hello World!"));
app.MapGet("/home", async context =>
        await context.Response.WriteAsync("Home Page!"));
app.MapGet("/home/index", async context =>
        await context.Response.WriteAsync("Home Index Page!"));

app.Run();
