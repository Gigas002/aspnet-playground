int id = 1; // для генерации id объектов

// начальные данные
var users = new List<Person>
{
    new() { Id = id++, Name = "Tom", Age = 37 },
    new() { Id = id++, Name = "Bob", Age = 41 },
    new() { Id = id++, Name = "Sam", Age = 24 }
};
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
app.MapGet("/api/users", () => users);
 
app.MapGet("/api/users/{id}", (int id) =>
{
    // получаем пользователя по id
    var user = users.FirstOrDefault(u => u.Id == id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, отправляем его
    return Results.Json(user);
});
 
app.MapDelete("/api/users/{id}", (int id) =>
{
    // получаем пользователя по id
    var user = users.FirstOrDefault(u => u.Id == id);
 
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, удаляем его
    users.Remove(user);
    return Results.Json(user);
});
 
app.MapPost("/api/users", (Person user) =>
{
    // устанавливаем id для нового пользователя
    user.Id = id++;
    // добавляем пользователя в список
    users.Add(user);
    return user;
});
 
app.MapPut("/api/users", (Person userData) =>
{
    // получаем пользователя по id
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    // если пользователь найден, изменяем его данные и отправляем обратно клиенту
 
    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

app.MapPost("/data", async(HttpContext httpContext) =>
{
    // получаем данные формы
    var form = httpContext.Request.Form; 
    string? name = form["name"];
    string? email = form["email"];
    string? age = form["age"];
    await httpContext.Response.WriteAsync($"Name: {name}   Email:{email}    Age: {age}");
});
 
app.Run();
 
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
