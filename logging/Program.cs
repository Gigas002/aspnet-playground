using Microsoft.Extensions.Logging.Debug;

var builder = WebApplication.CreateBuilder(args);

// builder.Logging.ClearProviders();   // удаляем все провайдеры
// builder.Logging.AddConsole();   // добавляем провайдер для логгирования на консоль

// устанавливаем файл для логгирования
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
// настройка логгирования с помошью свойства Logging идет до 
// создания объекта WebApplication

var app = builder.Build();

// app.Run(async (context) =>
// {
//     // пишем на консоль информацию
//     app.Logger.LogInformation($"Processing request {context.Request.Path}");
 
//     await context.Response.WriteAsync("Hello World!");
// });

// app.Map("/hello", (ILogger<Program> logger) =>
// {
//     logger.LogInformation($"Path: /hello  Time: {DateTime.Now.ToLongTimeString()}");
//     return "Hello World";
// });

// app.Run(async (context) =>
// {
//     var path = context.Request.Path;
//     app.Logger.LogCritical($"LogCritical {path}");
//     app.Logger.LogError($"LogError {path}");
//     app.Logger.LogInformation($"LogInformation {path}");
//     app.Logger.LogWarning($"LogWarning {path}");
 
//     await context.Response.WriteAsync("Hello World!");
// });

// ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
// ILogger logger = loggerFactory.CreateLogger<Program>();
// app.Run(async (context) =>
// {
//     logger.LogInformation($"Requested Path: {context.Request.Path}");
//     await context.Response.WriteAsync("Hello World!");
// });

// var loggerFactory = LoggerFactory.Create(builder =>
// {
//     builder.AddDebug();
//     builder.AddConsole();
//     // настройка фильтров
//     builder.AddFilter("System", LogLevel.Information)
//             .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace);
// });
// ILogger logger = loggerFactory.CreateLogger("WebApplication");

// app.Map("/hello", (ILoggerFactory loggerFactory)=>{
 
//     // создаем логгер с категорией "MapLogger"
//     ILogger logger = loggerFactory.CreateLogger("MapLogger");
//     // логгируем некоторое сообщение
//     logger.LogInformation($"Path: /hello   Time: {DateTime.Now.ToLongTimeString()}");
//     return "Hello World!";
// });

app.Run(async (context) =>
{
    app.Logger.LogInformation($"Path: {context.Request.Path}  Time:{DateTime.Now.ToLongTimeString()}");
    await context.Response.WriteAsync("Hello World!");
});

app.Run();

public class FileLogger : ILogger, IDisposable
{
    string filePath;
    static object _lock = new object();
    public FileLogger(string path)
    {
        filePath = path;
    }
    public IDisposable BeginScope<TState>(TState state)
    {
        return this;
    }
 
    public void Dispose() { }
 
    public bool IsEnabled(LogLevel logLevel)
    {
        //return logLevel == LogLevel.Trace;
        return true;
    }
 
    public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
        }
    }
}

public class FileLoggerProvider : ILoggerProvider
{
    string path;
    public FileLoggerProvider(string path)
    {
        this.path = path;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path);
    }
 
    public void Dispose() {}
}

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
    {
        builder.AddProvider(new FileLoggerProvider(filePath));
        return builder;
    }
}
