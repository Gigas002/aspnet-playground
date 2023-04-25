using Microsoft.Extensions.FileProviders;

// var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions { WebRootPath = "static"});  // изменяем папку для хранения статики

var app = builder.Build();

// DefaultFilesOptions options = new DefaultFilesOptions();
// options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
// options.DefaultFileNames.Add("hello.html"); // добавляем новое имя файла
// app.UseDefaultFiles(options); // установка параметров

// app.UseDefaultFiles();  // поддержка страниц html по умолчанию
// app.UseDirectoryBrowser();

// app.UseDirectoryBrowser(new DirectoryBrowserOptions()
// {
//     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
 
//     RequestPath = new PathString("/pages")
// });

// app.UseStaticFiles();   // добавляем поддержку статических файлов
// app.UseStaticFiles(new StaticFileOptions() // обрабатывает запросы к каталогу wwwroot/html
// {
//     FileProvider = new PhysicalFileProvider(
//             Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//     RequestPath = new PathString("/pages")
// });

app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
    RequestPath = new PathString("/pages"),
    EnableDefaultFiles = false
});

app.Run(async (context) => await context.Response.WriteAsync("Hello World"));

app.Run();
