using System.Text.Json;
using System.Text.Json.Serialization;

namespace DbLibrary;

public class BooksJsonConverter : JsonConverter<List<Book>>
{
    public override List<Book> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

        var list = new List<Book>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray) break;

            var id = reader.GetInt32();
            
            list.Add(new Book() { Id = id });
        }

        return list;
    }

    public override void Write(Utf8JsonWriter writer, List<Book> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var entry in value)
        { 
            writer.WriteNumberValue(entry.Id);
        }

        writer.WriteEndArray();
    }
}