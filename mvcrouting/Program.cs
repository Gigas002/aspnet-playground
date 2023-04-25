var builder = WebApplication.CreateBuilder(args);
 
// добавляем поддержку контроллеров с представлениями
builder.Services.AddControllersWithViews();
 
var app = builder.Build();
 
// // устанавливаем сопоставление маршрутов с контроллерами
// app.MapControllerRoute(
//     name: "default", 
//     pattern: "{controller=Home}/{action=Index}/{id:int?}");
// app.MapControllerRoute(name: "name_age", pattern: "{controller}/{action}/{name}/{age}");

// // добавляем поддержку контроллеров, которые располагаются в области
// app.MapControllerRoute(
//     name: "Account",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// маршрут для области account
app.MapAreaControllerRoute(
    name: "account_area",
    areaName: "account",
    pattern: "profile/{controller=Home}/{action=Index}/{id?}");

// добавляем поддержку для контроллеров, которые располагаются вне области
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
 
app.Run();