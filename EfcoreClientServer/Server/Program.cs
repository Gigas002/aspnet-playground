using DbLibrary;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Server;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel((_, options) =>
        {
            options.ListenAnyIP(5230, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });

        builder.Services.AddDbContext<Context>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        Context.Initialize();

        app.MapControllers();

        await app.RunAsync();
    }
}
