using System.Text.Json;
using System.Text.Json.Serialization;

namespace DbLibrary;

public class UserJsonConverter : JsonConverter<User>
{
    public override User Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetInt32();

        return new User { Id = id };
    }

    public override void Write(Utf8JsonWriter writer, User value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Id);
    }
}