using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

// [PrimaryKey(nameof(Id), nameof(Age))]
// [Index("PhoneNumber", "Passport", IsUnique = true)]
public class User
{
    // [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Passport { get; set; }
    public string? PhoneNumber { get; set; }
}
