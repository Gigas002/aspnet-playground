using System.Text.Json.Serialization;

namespace ChatGPT;

// TODO: simplify json attributes (net8.0)
// see: https://devblogs.microsoft.com/dotnet/announcing-dotnet-8-preview-1/#json-improvements

class Choice
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("message")]
    public Message Message { get; set; } = new();
    
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; } = "";
}
