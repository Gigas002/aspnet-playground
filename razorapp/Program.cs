using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// добавляем в приложение сервисы Razor Pages
// builder.Services.AddRazorPages(options => options.RootDirectory = "/MyPages");
builder.Services.AddRazorPages(options =>
{
    // отключаем глобально Antiforgery-токен
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddTransient<ITimeService, SimpleTimeService>();

var app = builder.Build();

// добавляем поддержку маршрутизации для Razor Pages
app.MapRazorPages();

app.Run();


public interface ITimeService
{
    string Time { get; }
}
public class SimpleTimeService : ITimeService
{
    public string Time => DateTime.Now.ToString("HH:mm:ss");
}
