var builder = WebApplication.CreateBuilder();
 
builder.Services.AddCors(); // добавляем сервисы CORS
 
var app = builder.Build();
 
// настраиваем CORS
app.UseCors(builder => builder.AllowAnyOrigin());
 
app.Map("/", async context => await context.Response.WriteAsync("Hello METANIT.COM!"));
 
app.Run();