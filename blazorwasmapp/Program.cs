using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazorwasmapp;  // пространство имен текущего проекта
 
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
 
await builder.Build().RunAsync();
