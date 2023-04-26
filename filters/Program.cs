using MvcApp.Filters;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllersWithViews();

// builder.Services.AddMvc(options =>
// {
//     options.Filters.Add(typeof(SimpleResourceFilter)); // подключение по типу
 
//     // альтернативный вариант подключения
//     options.Filters.Add(new SimpleResourceFilter()); // подключение по объекту
//     options.Filters.Add<SimpleResourceFilter>();  // применение типизированного метода
// });

builder.Services.AddControllersWithViews(options => options.Filters.Add<GlobalResourceFilter>());

var app = builder.Build();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();
    
app.Run();
