using Microsoft.AspNetCore.Http.Connections; // пространство имен перечисления HttpTransportType
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddSignalR();
 
var app = builder.Build();
 
app.UseDefaultFiles();
app.UseStaticFiles();
 
app.MapHub<ChatHub>("/chat",
    options => {
        options.ApplicationMaxBufferSize = 128;
        options.TransportMaxBufferSize = 128;
        options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1);
        options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
});
app.Run();