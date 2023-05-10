using System.ComponentModel.DataAnnotations.Schema;
 
public class User
{
    // [Column("user_id")]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    // навигационное свойство
    public Company? Company { get; set; }
}
