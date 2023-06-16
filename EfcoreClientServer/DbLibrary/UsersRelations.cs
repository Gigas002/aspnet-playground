using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DbLibrary;

[PrimaryKey("UserId", "RelatedUserId")]
public class UsersRelations
{
    [ForeignKey("UserId")]
    [JsonPropertyName("UserId")]
    [JsonConverter(typeof(UserJsonConverter))]
    public User Origin { get; set; }
    
    [ForeignKey("RelatedUserId")]
    [JsonPropertyName("RelatedUserId")]
    [JsonConverter(typeof(UserJsonConverter))]
    public User Related { get; set; }
    
    public Relation Relation { get; set; }
}