using MvcApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opts =>
{
    opts.ModelBinderProviders.Insert(0, new EventModelBinderProvider());
    // opts.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider());
});

var app = builder.Build();
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
 
app.Run();
