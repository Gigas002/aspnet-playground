using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Manager> Managers { get; set; } = null!;
    
    public ApplicationContext()
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<User>().UseTphMappingStrategy();  // TPH
    // }
}
