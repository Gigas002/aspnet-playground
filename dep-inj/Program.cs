using System.Text;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddTransient<ITimeService, ShortTimeService>();
// builder.Services.AddTransient<TimeService>();
// builder.Services.AddTimeService();

// builder.Services.AddTransient<ITimeService, ShortTimeService>();
// builder.Services.AddTransient<TimeMessage>();

// builder.Services.AddTransient<ICounter, RandomCounter>();
// builder.Services.AddTransient<CounterService>();

// builder.Services.AddScoped<ICounter, RandomCounter>();
// builder.Services.AddScoped<CounterService>();

// builder.Services.AddSingleton<ICounter, RandomCounter>();
// builder.Services.AddSingleton<CounterService>();

// or

// RandomCounter rndCounter = new RandomCounter();
// builder.Services.AddSingleton<ICounter>(rndCounter);
// builder.Services.AddSingleton<CounterService>(new CounterService(rndCounter));

// builder.Services.AddTransient<TimeService>();

// builder.Services.AddTransient<ITimer, Timer>();
// builder.Services.AddScoped<TimerService>();

// builder.Services.AddTransient<IHelloService, RuHelloService>();
// builder.Services.AddTransient<IHelloService, EnHelloService>();

// builder.Services.AddSingleton<IGenerator, ValueStorage>();
// builder.Services.AddSingleton<IReader, ValueStorage>();

// builder.Services.AddSingleton<ValueStorage>();
// builder.Services.AddSingleton<IGenerator>(serv => serv.GetRequiredService<ValueStorage>());
// builder.Services.AddSingleton<IReader>(serv => serv.GetRequiredService<ValueStorage>());

// or

var valueStorage = new ValueStorage();
builder.Services.AddSingleton<IGenerator>(_ => valueStorage);
builder.Services.AddSingleton<IReader>(_ => valueStorage);

// var services = builder.Services;  // коллекция сервисов

var app = builder.Build();

// app.Run(async context =>
// {
//     var sb = new StringBuilder();
//     sb.Append("<h1>Все сервисы</h1>");
//     sb.Append("<table>");
//     sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");
//     foreach (var svc in services)
//     {
//         sb.Append("<tr>");
//         sb.Append($"<td>{svc.ServiceType.FullName}</td>");
//         sb.Append($"<td>{svc.Lifetime}</td>");
//         sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
//         sb.Append("</tr>");
//     }
//     sb.Append("</table>");
//     context.Response.ContentType = "text/html;charset=utf-8";
//     await context.Response.WriteAsync(sb.ToString());
// });

// app.Run(async context =>
// {
//     var timeService = app.Services.GetService<ITimeService>(); 
//     await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
// });

// app.Run(async context =>
// {
//     var timeService = app.Services.GetService<TimeService>();
//     await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
// });

// app.Run(async context =>
// {
//     var timeService = app.Services.GetService<TimeService>();
//     context.Response.ContentType = "text/html; charset=utf-8";
//     await context.Response.WriteAsync($"Текущее время: {timeService?.GetTime()}");
// });

// app.Run(async context =>
// {
//     var timeService = context.RequestServices.GetService<ITimeService>(); 
//     await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
// });

// app.Run(async context =>
// {
//     var timeMessage = context.RequestServices.GetService<TimeMessage>();
//     context.Response.ContentType = "text/html;charset=utf-8";
//     await context.Response.WriteAsync($"<h2>{timeMessage?.GetTime()}</h2>");
// });

// app.UseMiddleware<TimeMessageMiddleware>();

// app.UseMiddleware<CounterMiddleware>();

// app.UseMiddleware<TimerMiddleware>();
// app.Run(async (context) => await context.Response.WriteAsync("Hello METANIT.COM"));

// app.UseMiddleware<TimerMiddleware>();

// app.UseMiddleware<HelloMiddleware>();

app.UseMiddleware<GeneratorMiddleware>();
app.UseMiddleware<ReaderMiddleware>();

app.Run();

interface ITimeService
{
    string GetTime();
}

// время в формате hh::mm
class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}

// время в формате hh:mm:ss
class LongTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}

public class TimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
    public TimeService()
    {
        Time = DateTime.Now.ToLongTimeString();
    }
    public string Time { get; }
}

public static class ServiceProviderExtensions
{
    public static void AddTimeService(this IServiceCollection services)
    {
        services.AddTransient<TimeService>();
    }
}

class TimeMessage
{
    ITimeService timeService;
    public TimeMessage(ITimeService timeService)
    {
        this.timeService = timeService;
    }
    public string GetTime() => $"Time: {timeService.GetTime()}";
}

class TimeMessageMiddleware
{
    RequestDelegate next;
    ITimeService timeService;
    public TimeMessageMiddleware(RequestDelegate next, ITimeService timeService)
    {
        this.next = next;
        this.timeService = timeService;
    }
 
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync($"<h1>Time: {timeService.GetTime()}</h1>");
    }
}

public interface ICounter
{
    int Value { get; }
}

public class RandomCounter : ICounter
{
    static Random rnd = new Random();
    private int _value;
    public RandomCounter()
    {
        _value = rnd.Next(0, 1000000);
    }
    public int Value
    {
        get => _value;
    }
}

public class CounterService
{
    public ICounter Counter { get; }
    public CounterService(ICounter counter)
    {
        Counter = counter;
    }
}

public class CounterMiddleware
{
    RequestDelegate next;
    int i = 0; // счетчик запросов
    public CounterMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext, ICounter counter, CounterService counterService)
    {
        i++;
        httpContext.Response.ContentType = "text/html;charset=utf-8";
        await httpContext.Response.WriteAsync($"Запрос {i}; Counter: {counter.Value}; Service: {counterService.Counter.Value}");
    }
}

// public class TimerMiddleware
// {
//     RequestDelegate next;
 
//     public TimerMiddleware(RequestDelegate next)
//     {
//         this.next = next;
//     }
 
//     public async Task InvokeAsync(HttpContext context, TimeService timeService)
//     {
//         if (context.Request.Path == "/time")
//         {
//             context.Response.ContentType = "text/html; charset=utf-8";
//             await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
//         }
//         else
//         {
//             await next.Invoke(context);
//         }
//     }
// }

public interface ITimer
{
    string Time { get; }
}
public class Timer : ITimer
{
    public Timer()
    {
        Time = DateTime.Now.ToLongTimeString();
    }
    public string Time { get; }
}
public class TimerService
{
    private ITimer timer;
    public TimerService(ITimer timer)
    {
        this.timer = timer;
    }
    public string GetTime() =>  timer.Time;
}

public class TimerMiddleware
{
    TimerService timerService;
    public TimerMiddleware(RequestDelegate next, TimerService timerService)
    {
        this.timerService = timerService;
    }
 
    public async Task Invoke(HttpContext context)
    {
        await context.Response.WriteAsync($"Time: {timerService?.GetTime()}");
    }
}


interface IHelloService
{
    string Message { get; }
}
 
class RuHelloService : IHelloService
{
    public string Message => "Привет METANIT.COM";
}
class EnHelloService : IHelloService
{
    public string Message => "Hello METANIT.COM";
}
 
class HelloMiddleware
{
    readonly IEnumerable<IHelloService> helloServices;
 
    public HelloMiddleware(RequestDelegate _, IEnumerable<IHelloService> helloServices)
    {
        this.helloServices = helloServices;
    }
 
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        string responseText = "";
        foreach (var service in helloServices)
        {
            responseText += $"<h3>{service.Message}</h3>";
        }
        await context.Response.WriteAsync(responseText);
    }
}

interface IGenerator
{
    int GenerateValue();
}
interface IReader
{
    int ReadValue();
}
class ValueStorage : IGenerator, IReader
{
    int value;
    public int GenerateValue()
    {
        value = new Random().Next();
        return value;
    }
 
    public int ReadValue() => value;
}

class GeneratorMiddleware
{
    RequestDelegate next;
    IGenerator generator;
 
    public GeneratorMiddleware(RequestDelegate next, IGenerator generator)
    {
        this.next = next;
        this.generator = generator;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/generate")
            await context.Response.WriteAsync($"New Value: {generator.GenerateValue()}");
        else
            await next.Invoke(context);
    }
}
class ReaderMiddleware
{
    IReader reader;
 
    public ReaderMiddleware(RequestDelegate _, IReader reader) => this.reader = reader;
     
    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.WriteAsync($"Current Value: {reader.ReadValue()}");
    }
}
