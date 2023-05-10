// страна компании
public class Country
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CapitalId { get; set; }
    public City? Capital { get; set; }  // столица страны
    public List<Company> Companies { get; set; } = new();
}
