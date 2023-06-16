using System.Text.Json.Serialization;

namespace DbLibrary;

public class Book
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    [JsonConverter(typeof(UsersJsonConverter))]
    // [JsonIgnore]
    public List<User> Users { get; set; } = new();
}
