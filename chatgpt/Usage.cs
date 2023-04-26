namespace ChatGPT;

public record class Usage(int PromptTokens, int CompletionTokens, int TotalTokens);
