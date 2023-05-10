using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class ApplicationContext : DbContext
{
    readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);
    public DbSet<User> Users { get; set; } = null!;
    
    // public ApplicationContext()
    // {
    //     // Database.EnsureDeleted();
    //     // Database.EnsureCreated();
    // }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlite("Data Source=helloapp2.db");
        optionsBuilder.LogTo(logStream.WriteLine);
        // optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
    }

    public override void Dispose()
    {
        base.Dispose();
        logStream.Dispose();
    }
 
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await logStream.DisposeAsync();
    }
}
