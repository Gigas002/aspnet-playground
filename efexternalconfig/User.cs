using Microsoft.EntityFrameworkCore;

// [EntityTypeConfiguration(typeof(UserConfiguration))]
public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string Taras { get; set; } = "Panis";
}
