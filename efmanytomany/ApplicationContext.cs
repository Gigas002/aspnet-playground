using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudentRelations> StudentRelations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentRelations>()
                    .HasOne(sr => sr.Student)
                    .WithMany(s => s.Relations);

        modelBuilder.Entity<StudentRelations>()
                    .HasOne(sr => sr.RelatedStudent);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=students.db")
                      .UseSnakeCaseNamingConvention();;
    }
}
