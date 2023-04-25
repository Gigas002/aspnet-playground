using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
 
// // добавляем сервисы сжатия
// builder.Services.AddResponseCompression(options =>
// {
//     options.EnableForHttps = true;
//     // исключаем из сжатия простой текст
//     options.ExcludedMimeTypes = new[] { "text/plain" };
// });

// // устанавливаем уровень сжатия
// builder.Services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

// builder.Services.AddResponseCompression(options =>
// {
//     options.EnableForHttps = true;
//     // добавляем провайдер сжатия DeflateCompressionProvider
//     options.Providers.Add(new DeflateCompressionProvider());
// });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=600");
    }
});
 
// // подключаем сжатие
// app.UseResponseCompression();
 
// app.MapGet("/", () => "Lorem Ipsum is simply dummy text of the printing and typesetting industry...exact original form, accompanied by English versions from the 1914 translation by H. Rackham.");
 
app.Run();

public class DeflateCompressionProvider : ICompressionProvider
{
    public string EncodingName => "deflate";
    public bool SupportsFlush => true;
    public Stream CreateStream(Stream outputStream)
    {
        return new DeflateStream(outputStream, CompressionLevel.Optimal);
    }
}
