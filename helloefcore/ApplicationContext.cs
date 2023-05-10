using Microsoft.EntityFrameworkCore;
 
public class ApplicationContext : DbContext
{
    // public DbSet<User> Users => Set<User>();

    public DbSet<User> Users { get; set; } = null!;

    // public ApplicationContext() 
    // {
    //     // Database.EnsureDeleted();
    //     Database.EnsureCreated();
    // }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=helloapp.db");
    // }
}
