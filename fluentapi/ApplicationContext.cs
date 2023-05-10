using Microsoft.EntityFrameworkCore;
 
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext()
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     // использование Fluent API
    //     base.OnModelCreating(modelBuilder);
    // }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Ignore<Company>();
    //     modelBuilder.Entity<Country>();
    // }
}