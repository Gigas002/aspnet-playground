using Microsoft.EntityFrameworkCore;
 
public class ApplicationContext : DbContext
{
    private readonly StreamWriter _logStream = new StreamWriter("mylog.txt", true);

    public DbSet<User> Users { get; set; } = null!;
 
    // public ApplicationContext()
    // {
    //     Database.EnsureCreated();
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=gigas");
        optionsBuilder.LogTo(_logStream.WriteLine);
    }

    public override void Dispose()
    {
        base.Dispose();
        _logStream.Dispose();
    }
 
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _logStream.DisposeAsync();
    }
}
