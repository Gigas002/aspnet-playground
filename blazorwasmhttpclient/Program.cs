using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazorwasmhttpclient;  // пространство имен текущего проекта
 
var builder = WebAssemblyHostBuilder.CreateDefault(args);
// добавляем HttpClient
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5153/")
    });
 
builder.RootComponents.Add<App>("#app");
 
await builder.Build().RunAsync();