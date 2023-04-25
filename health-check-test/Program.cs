using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
 
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddHealthChecks()
    .AddCheck<RequestTimeHealthCheck>("RequestTimeCheck");  // проверяем работоспособность с RequestTimeCheck
 
builder.Services.AddHttpClient();   // подключаем HttpClient
builder.WebHost.UseUrls("http://[::]:44444");  // обрабатываем запросы по адресу https://localhost:44444
 
var app = builder.Build();
app.MapHealthChecks("/health");
 
app.MapGet("/", async (HttpClient httpClient) =>
{
    // отправляем запрос к другому сервису и возвращаем его ответ
    var response = await httpClient.GetAsync("https://localhost:33333/data");
    return await response.Content.ReadAsStringAsync();
});
 
app.Run();
 
public class RequestTimeHealthCheck : IHealthCheck
{
    int degraded_level = 2000;  // уровень плохой работы
    int unhealthy_level = 5000; // нерабочий уровень
    HttpClient httpClient;
    public RequestTimeHealthCheck(HttpClient client) => httpClient = client;
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        // получаем время запроса
        Stopwatch sw = Stopwatch.StartNew();
        await httpClient.GetAsync("https://localhost:33333/data");
        sw.Stop();
        var responseTime = sw.ElapsedMilliseconds;
        // в зависимости от времени запроса возвращаем определенный результат
        if (responseTime < degraded_level)
        {
            return HealthCheckResult.Healthy("Система функционирует хорошо");
        }
        else if (responseTime < unhealthy_level)
        {
            return HealthCheckResult.Degraded("Снижение качества работы системы");
        }
        else
        {
            return HealthCheckResult.Unhealthy("Система в нерабочем состоянии. Необходим ее перезапуск.");
        }
    }
}
