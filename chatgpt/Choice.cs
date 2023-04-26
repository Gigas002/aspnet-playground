namespace ChatGPT;

public record class Choice(int Index, Message Message, string FinishReason);
