using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// внедрение зависимости Entity Framework
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=usercacheapp.db"));
// внедрение зависимости UserService
builder.Services.AddTransient<UserService>();
// добавление кэширования
builder.Services.AddMemoryCache();
var app = builder.Build();

app.MapGet("/user/{id}", async (int id, UserService userService) =>
{
    User? user = await userService.GetUser(id);
    if (user != null) return $"User {user.Name}  Id={user.Id}  Age={user.Age}";
    return "User not found";
});
app.MapGet("/", () => "Hello World!");

app.Run();


public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) =>
        Database.EnsureCreated();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // инициализация БД начальными данными
        modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Age = 23 },
                new User { Id = 2, Name = "Alice", Age = 26 },
                new User { Id = 3, Name = "Sam", Age = 28 }
        );
    }
}
public class UserService
{
    ApplicationContext db;
    IMemoryCache cache;
    public UserService(ApplicationContext context, IMemoryCache memoryCache)
    {
        db = context;
        cache = memoryCache;
    }
    public async Task<User?> GetUser(int id)
    {
        // пытаемся получить данные из кэша
        cache.TryGetValue(id, out User? user);

        // если данные не найдены в кэше
        if (user == null)
        {
            // обращаемся к базе данных
            user = await db.Users.FindAsync(id);
            // если пользователь найден, то добавляем в кэш
            if (user != null)
            {
                Console.WriteLine($"{user.Name} извлечен из базы данных");

                // определяем параметры кэширования
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    // кэширование в течение 1 минуты
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    // низкий приоритет
                    Priority = 0,
                };
                // определяем коллбек при удалении записи из кэша
                var callbackRegistration = new PostEvictionCallbackRegistration();
                callbackRegistration.EvictionCallback =
                    (object key, object? value, EvictionReason reason, object? state) => Console.WriteLine($"запись {id} устарела");
                cacheOptions.PostEvictionCallbacks.Add(callbackRegistration);

                cache.Set(user.Id, user, cacheOptions);
            }
        }
        else
        {
            Console.WriteLine($"{user.Name} извлечен из кэша");
        }
        return user;
    }
}