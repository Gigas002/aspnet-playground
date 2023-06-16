using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DbLibrary;

public class UsersNames
{
    public int Id { get; set; }

    public string Value { get; set; }
    
    [JsonConverter(typeof(UserJsonConverter))]
    // [JsonPropertyName("UserId")]
    // [JsonIgnore]
    [ForeignKey("UserId")]
    public User User { get; set; }

    public UsersNames() { }

    public UsersNames(User user, string value) => (User, Value) = (user, value);
}
