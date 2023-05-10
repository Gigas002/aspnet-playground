using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Company> Companies { get; set; } = null!;

    public ApplicationContext()
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
 
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());

        // or

        // modelBuilder.Entity<User>(UserConfigure);
        // modelBuilder.Entity<Company>(CompanyConfigure);
    }

    // конфигурация для типа User
    public void UserConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("People").Property(p => p.Name).IsRequired();
        builder.Property(p => p.Id).HasColumnName("user_id");
    }

    // конфигурация для типа Company
    public void CompanyConfigure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Enterprises").Property(c => c.Name).IsRequired();
    }
}
