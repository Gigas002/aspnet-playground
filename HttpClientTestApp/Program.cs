var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    Console.WriteLine("Enter into /");

    // пытаемся получить заголовок "SecreteCode"
    context.Request.Headers.TryGetValue("User-Agent", out var userAgent);
    // пытаемся получить заголовок "SecreteCode"
    context.Request.Headers.TryGetValue("SecreteCode", out var secreteCode);
    // отправляем данные обратно клиенту
    return $"User-Agent: {userAgent}    SecreteCode: {secreteCode}";
});

app.MapGet("/{id}", (int id) =>
{
    Console.WriteLine($"Enter into /{id}");

    // if (id is null)
    //     return Results.BadRequest(new { Message = "Некорректные данные в запросе" });
    if (id != 1)
        return Results.NotFound(new { Message = $"Объект с id={id} не существует" });
    else
        return Results.Json(new Person("Bob", 42));
});

app.MapPost("/data", async (HttpContext httpContext) =>
{
    Console.WriteLine("Enter into /data");

    using var reader = new StreamReader(httpContext.Request.Body);
    var name = await reader.ReadToEndAsync();

    return $"Получены данные: {name}";
});

app.MapPost("/create", (Person person) =>
{
    Console.WriteLine("Enter into /create");

    // устанавливает id у объекта Person
    person.Id = Guid.NewGuid().ToString();
    // отправляем обратно объект Person
    return person;
});

app.Run();

class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }

    public Person() { }
    public Person(string name, int age) => (Name, Age) = (name, age);
}
