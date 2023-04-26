namespace ChatGPT;

public record class ResponseData(string Id, string Object, ulong Created,
                                 List<Choice> Choices, Usage Usage);
