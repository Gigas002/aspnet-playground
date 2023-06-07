var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddOpenApiDocument();
builder.Services.AddSwaggerDocument();

var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUi3();
// app.UseReDoc();

app.UseRouting();

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Cookies.Append("name", "Tom");
    context.Response.Cookies.Append("email", "tom@localhost.com");
});

app.MapGet("/recieve", (HttpContext context) =>
{
    // получаем куки
    context.Request.Cookies.TryGetValue("name", out string? name);
    context.Request.Cookies.TryGetValue("email", out string? email);

    return $"Name: {name}   Email:{email}";
});

app.Run();
