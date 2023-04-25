var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();  // добавляем поддержку контроллеров
builder.Services.AddTransient<ITimeService, SimpleTimeService>(); // добавляем сервис ITimeService

var app = builder.Build();

// устанавливаем сопоставление маршрутов с контроллерами
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public interface ITimeService
{
    string Time { get; }
}
public class SimpleTimeService: ITimeService
{
    public string Time => DateTime.Now.ToString("hh:mm:ss");
}
