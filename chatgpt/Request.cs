namespace ChatGPT;

public record class Request(string Model, List<Message> Messages);
