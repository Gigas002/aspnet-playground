using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazorwasmcrudclient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// добавляем HttpClient
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5223/")
    });
 
builder.RootComponents.Add<App>("#app");
 
await builder.Build().RunAsync();
