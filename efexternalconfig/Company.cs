using Microsoft.EntityFrameworkCore;

// [EntityTypeConfiguration(typeof(CompanyConfiguration))]
public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
