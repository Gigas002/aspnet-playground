using System.Text.Json.Serialization;

namespace ChatGPT;

class ResponseData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("object")]
    public string Object { get; set; } = "";

    [JsonPropertyName("created")]
    public ulong Created { get; set; }

    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = new();
    
    [JsonPropertyName("usage")]
    public Usage Usage { get; set; } = new();
}
