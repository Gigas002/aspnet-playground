var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITimeService, SimpleTimeService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.Run();

public interface ITimeService
{
    string GetTime();
}
public class SimpleTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToString("HH:mm:ss");
}
