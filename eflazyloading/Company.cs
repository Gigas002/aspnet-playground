public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual List<User> Users { get; set; } = new ();
}
