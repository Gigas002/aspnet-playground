public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    public int? PositionId { get; set; }
    public Position? Position { get; set; }
}
