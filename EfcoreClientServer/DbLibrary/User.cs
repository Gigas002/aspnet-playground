using System.Text.Json.Serialization;

namespace DbLibrary;

public class User
{
    public int Id { get; set; }

    public int Age { get; set; }

    // [JsonIgnore]
    [JsonConverter(typeof(BooksJsonConverter))]
    public List<Book> Books { get; set; } = new();

    [JsonIgnore]
    public List<UsersNames> UserNames { get; set; } = new();

    public List<UsersRelations> UsersRelations { get; set; } = new();

    public void AddName(string name) => UserNames.Add(new UsersNames(this, name));
}