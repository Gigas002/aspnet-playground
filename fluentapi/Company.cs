using System.ComponentModel.DataAnnotations.Schema;

// [NotMapped]
public class Company
{
    public int Id { get; set; }
    [NotMapped]
    public string? Name { get; set; }
}