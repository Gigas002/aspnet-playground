using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace DbLibrary;

public class Context : DbContext
{
    internal const string LogPath = "../log.txt";
    private readonly StreamWriter _logStream = new(LogPath, true);
    private const string SqliteDatabasePath = "../udb.db";

    public DbSet<User> Users { get; set; }
    
    public DbSet<Book> Books { get; set; }

    public DbSet<UsersNames> UsersNames { get; set; }
    
    public DbSet<UsersRelations> UsersRelations { get; set; }

    public string DatabasePath { get; init; } = null!;

    public Context(string databasePath = SqliteDatabasePath) => DatabasePath = databasePath;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}")
                      .LogTo(_logStream.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersRelations>()
                    .HasOne(cr => cr.Origin)
                    .WithMany(c => c.UsersRelations);
    }

    public static void Initialize()
    {
        using var context = new Context();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var user1 = new User { Age = 20 };
        user1.AddName("Tom");
        user1.AddName("Tommy");

        var user2 = new User { Age = 21 };
        user2.AddName("Bob");
        user2.AddName("Bobby");
        
        var book1 = new Book { Title = "Title1" };
        var book2 = new Book { Title = "Title2" };
        
        user1.Books.Add(book1);
        book2.Users.Add(user2);
        
        context.Users.AddRange(user1, user2);
        context.Books.AddRange(book1, book2);

        context.SaveChanges();
    }

    public static User GetUser(Context? context, int id)
    {
        bool disposable = context is null;

        if (disposable) context = new Context();

        var user = context.Users.Find(id);

        if (disposable) context.Dispose();

        return user;
    }

    public static Book GetBook(Context? context, int id)
    {
        bool disposable = context is null;

        if (disposable) context = new Context();

        var book = context.Books.Find(id);

        if (disposable) context.Dispose();

        return book;
    }

    public static IEnumerable<Book> GetUserBooks(Context? context, int id)
    {
        bool disposable = context is null;

        if (disposable) context = new Context();

        var user = context.Users.Include(u => u.Books)
                          .FirstOrDefault(u => u.Id == id);

        if (disposable) context.Dispose();

        return user.Books;
    }
    
    public static IEnumerable<UsersRelations> GetUserRelations(Context? context, int id)
    {
        bool disposable = context is null;

        if (disposable) context = new Context();

        var user = context.Users.Include(u => u.UsersRelations)
                          .ThenInclude(u => u.Related)
                          .FirstOrDefault(u => u.Id == id);

        if (disposable) context.Dispose();

        return user.UsersRelations;
    }
    
    public static IEnumerable<UsersNames> GetUserNames(Context? context, int id)
    {
        bool disposable = context is null;

        if (disposable) context = new Context();

        var user = context.Users.Include(u => u.UserNames)
                          .FirstOrDefault(u => u.Id == id);

        if (disposable) context.Dispose();

        return user.UserNames;
    }

    public static async Task<FileStream> SerializeUsersAsync()
    {
        var jsonPath = "../users.json";
        
        await using var context = new Context();

        var users = context.Users.Include(u => u.UserNames)
                           .Include(u => u.Books);

        var stream = File.Open(jsonPath, FileMode.OpenOrCreate);
        
        var options = new JsonSerializerOptions();
        await JsonSerializer.SerializeAsync(stream, users, options);
        
        return stream; 
    }

    public static ValueTask<T> DeserializeAsync<T>(Stream jsonStream) where T : class
    {
        var options = new JsonSerializerOptions();
        var users = JsonSerializer.DeserializeAsync<T>(jsonStream, options);

        return users;
    }
}
