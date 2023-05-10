namespace blazorhttpclient;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddCors();
        
        var app = builder.Build();
        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        
        app.MapPost("/user", (Person user) =>
        {
            // если длина имени меньше 3 или больше 20 символов
            if(user.Name.Length < 3 || user.Name.Length > 20) 
                return Results.BadRequest(new {details="Имя должно иметь не меньше 3 и не больше 20 символов" });
            // если возраст меньше 1 или больше 110
            if (user.Age < 1 || user.Age > 110)
                return Results.BadRequest(new {details = "Некорректный возраст" });
            // если все нормально, устанавливаем id для нового пользователя
            user.Id = Guid.NewGuid().ToString();
            // посылаем объект в виде json
            return Results.Json(user);
        });
        
        app.Run();
    }
}

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
