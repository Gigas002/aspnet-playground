using Microsoft.Extensions.Options;

// string[] commandLineArgs = { "name=Alice", "age=29" };  // псевдопараметры командной строки
// var builder = WebApplication.CreateBuilder(commandLineArgs);

var builder = WebApplication.CreateBuilder(args);

// string[] commandLineArgs = { "name=Sam", "age=25" };  // псевдопараметры командной строки
// builder.Configuration.AddCommandLine(commandLineArgs);  // передаем параметры в качестве конфигурации

// builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
// {
//     {"name", "Tom"},
//     {"age", "37"}
// });

builder.Configuration.AddJsonFile("person.json");
// var tom = new Person();
// app.Configuration.Bind(tom);    // связываем конфигурацию с объектом tom

// устанавливаем объект Person по настройкам из конфигурации
builder.Services.Configure<Person>(builder.Configuration);
// builder.Services.Configure<Person>(opt =>
// {
//     opt.Age = 22;
// });

var app = builder.Build();

// // установка настроек конфигурации
// app.Configuration["name"] = "Tom";
// app.Configuration["age"] = "37";
 
// app.Run(async (context) =>
// {
//     // получение настроек конфигураци
//     string name = app.Configuration["name"];
//     string age = app.Configuration["age"];
//     await context.Response.WriteAsync($"{name} - {age}");
// });

// builder.Configuration.AddJsonFile("config.json");
// builder.Configuration.AddJsonFile("project.json");
// builder.Configuration.AddTextFile("config.txt");

// через механизм внедрения зависимостей получим сервис IConfiguration
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
// app.Map("/", (IConfiguration appConfig) => $"JAVA_HOME: {appConfig["JAVA_HOME"] ?? "not set"}");
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");

// app.Map("/", (IConfiguration appConfig) =>
// {
//     var personName = appConfig["person:profile:name"];
//     var companyName = appConfig["company:name"];
//     return $"{personName} - {companyName}";
// });

// app.Map("/", (IConfiguration appConfig) => GetSectionContent(appConfig.GetSection("projectConfig")));
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
// app.Map("/", (IConfiguration appConfig) =>
// {
//     var tom = appConfig.Get<Person>();  // связываем конфигурацию с объектом tom
//     return $"{tom.Name} - {tom.Age}";
// });

// app.Run(async (context) =>
// {
//     context.Response.ContentType = "text/html; charset=utf-8";
//     string name = $"<p>Name: {tom.Name}</p>";
//     string age = $"<p>Age: {tom.Age}</p>";
//     string company = $"<p>Company: {tom.Company?.Title}</p>";
//     string langs = "<p>Languages:</p><ul>";
//     foreach (var lang in tom.Languages)
//     {
//         langs += $"<li><p>{lang}</p></li>";
//     }
//     langs += "</ul>";
 
//     await context.Response.WriteAsync($"{name}{age}{company}{langs}");
// });

// app.Map("/", (IOptions<Person> options) =>
// {
//     Person person = options.Value;  // получаем переданные через Options объект Person
//     return person;
// });

app.UseMiddleware<PersonMiddleware>();

app.Run();

string GetSectionContent(IConfiguration configSection)
{
    System.Text.StringBuilder contentBuilder = new();
    foreach (var section in configSection.GetChildren())
    {
        contentBuilder.Append($"\"{section.Key}\":");
        if (section.Value == null)
        {
            string subSectionContent = GetSectionContent(section);
            contentBuilder.Append($"{{\n{subSectionContent}}},\n");
        }
        else
        {
            contentBuilder.Append($"\"{section.Value}\",\n");
        }
    }
    return contentBuilder.ToString();
}

public class TextConfigurationProvider : ConfigurationProvider
{
    public string FilePath { get; set; }
    public TextConfigurationProvider(string path)
    {
        FilePath = path;
    }
    public override void Load()
    {
        var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (StreamReader textReader = new StreamReader(FilePath))
        {
            string? line;
            while ((line = textReader.ReadLine()) != null)
            {
                string key = line.Trim();
                string? value = textReader.ReadLine() ?? "";
                data.Add(key, value);
            }
        }
        Data = data;
    }
}

public class TextConfigurationSource : IConfigurationSource
{
    public string FilePath { get; }
    public TextConfigurationSource(string filename)
    {
        FilePath = filename;
    }
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        // получаем полный путь для файла
        string filePath = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
        return new TextConfigurationProvider(filePath);
    }
}

public static class TextConfigurationExtensions
{
    public static IConfigurationBuilder AddTextFile(
        this IConfigurationBuilder builder, string path)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Путь к файлу не указан");
        }
 
        var source = new TextConfigurationSource(path);
        builder.Add(source);
        return builder;
    }
}

public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public List<string> Languages { get; set; } = new();
    public Company? Company { get; set; }
}
public class Company
{
    public string Title { get; set; } = "";
    public string Country { get; set; } = "";
}

public class PersonMiddleware
{
    private readonly RequestDelegate _next;
    public Person Person { get; }
    public PersonMiddleware(RequestDelegate next, IOptions<Person> options)
    {
        _next = next;
        Person = options.Value;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        System.Text.StringBuilder stringBuilder = new();
        stringBuilder.Append($"<p>Name: {Person.Name}</p>");
        stringBuilder.Append($"<p>Age: {Person.Age}</p>");
        stringBuilder.Append($"<p>Company: {Person.Company?.Title}</p>");
        stringBuilder.Append("<h3>Languages</h3><ul>");
        foreach (string lang in Person.Languages)
            stringBuilder.Append($"<li>{lang}</li>");
        stringBuilder.Append("</ul>");
 
        await context.Response.WriteAsync(stringBuilder.ToString());
    }
}
