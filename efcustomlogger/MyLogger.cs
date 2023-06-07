using Microsoft.Extensions.Logging;

public class MyLogger : ILogger, IDisposable
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return this;
    }

    public void Dispose() { }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        File.AppendAllText("log.txt", formatter(state, exception));
        Console.WriteLine(formatter(state, exception));
    }
}
