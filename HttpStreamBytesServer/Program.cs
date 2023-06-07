var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
app.MapPost("/data", async(HttpContext httpContext) =>
{
    // путь к папке, где будут храниться файлы
    var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
    // создаем папку для хранения файлов
    Directory.CreateDirectory(uploadPath);
    // генерируем произвольное название файла с помощью guid
    string fileName = Guid.NewGuid().ToString();
    // получаем поток
    using (var fileStream = new FileStream($"{uploadPath}/{fileName}.webp", FileMode.Create))
    {
        await httpContext.Request.Body.CopyToAsync(fileStream);
    }
 
    await httpContext.Response.WriteAsync("Данные сохранены");
});

app.MapPost("/bata", async(HttpContext httpContext) =>
{
    // считываем полученные данные в строку
    using StreamReader streamReader = new StreamReader(httpContext.Request.Body);
    string message = await streamReader.ReadToEndAsync();  
    await httpContext.Response.WriteAsync($"Отправлено сообщение: {message}");
});

app.MapPost("/upload", async (HttpContext context) =>
{
    var form = context.Request.Form;
    // получаем отдельные данные
    string? username = form["username"];
    string? email = form["email"];

    // получем коллецию загруженных файлов
    IFormFileCollection files = context.Request.Form.Files;
    // путь к папке, где будут храниться файлы
    var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
    // создаем папку для хранения файлов
    Directory.CreateDirectory(uploadPath);
 
    // пробегаемся по всем файлам
    foreach (var file in files)
    {
        // формируем путь к файлу в папке uploads
        string fullPath = $"{uploadPath}/{file.FileName}";
 
        // сохраняем файл в папку uploads
        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }

    return $"Данные пользователя {username} ({email}) успешно загружены";

    // await context.Response.WriteAsync("Файлы успешно загружены");
});
 
app.Run();
