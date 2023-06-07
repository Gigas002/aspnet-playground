using Microsoft.Extensions.Logging;

public class MyLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new MyLogger();
    }
    public void Dispose() { }

}
