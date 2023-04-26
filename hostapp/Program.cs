var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddRazorPages();   // добавляем поддержку Razor Pages
 
var app = builder.Build();
 
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
 
app.MapFallbackToPage("/_Host");
 
app.Run();