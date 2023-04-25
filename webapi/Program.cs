// начальные данные
List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
};
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
app.UseDefaultFiles();
app.UseStaticFiles();
 
app.MapGet("/api/users", ()=> users);
 
app.MapGet("/api/users/{id}", (string id) =>
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault(u => u.Id == id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null)  return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, отправляем его
    return Results.Json(user);
});
 
app.MapDelete("/api/users/{id}", (string id) =>
{
    // получаем пользователя по id
    Person? user = users.FirstOrDefault(u => u.Id == id);
 
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, удаляем его
    users.Remove(user);
    return Results.Json(user);
});
 
app.MapPost("/api/users", (Person user)=>{
 
    // устанавливаем id для нового пользователя
    user.Id = Guid.NewGuid().ToString();
    // добавляем пользователя в список
    users.Add(user);
    return user;
});
 
app.MapPut("/api/users", (Person userData) => {
 
    // получаем пользователя по id
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    // если пользователь найден, изменяем его данные и отправляем обратно клиенту
     
    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});
 
app.Run();
 
public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}