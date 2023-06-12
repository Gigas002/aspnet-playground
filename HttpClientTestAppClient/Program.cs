using System.Net;
using System.Net.Http.Json;
class Program
{
    public const string IPv6ServerAddress =  "https://[::1]:5230";

    public const string ServerAddress = "https://localhost:5230";
    
    public const string CloudflareHttp3ServerAddress = "https://cloudflare-quic.com";

    static HttpClient httpClient = new HttpClient() 
    {
        DefaultRequestVersion = HttpVersion.Version30,
        DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact
    };
    
    static async Task Main()
    {
        // устанавливаем оба заголовка
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        httpClient.DefaultRequestHeaders.Add("SecreteCode", "hello");
 
        using var response = await httpClient.GetAsync(ServerAddress);
        
        response.Headers.TryGetValues("date", out var dateValues);
        Console.WriteLine(dateValues?.FirstOrDefault());
        
        var responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        using var getResponse = await httpClient.GetAsync($"{ServerAddress}/1"); 

        if (getResponse.StatusCode == HttpStatusCode.BadRequest || getResponse.StatusCode == HttpStatusCode.NotFound)
        {
            // получаем информацию об ошибке
            Error? error = await getResponse.Content.ReadFromJsonAsync<Error>();
            Console.WriteLine(getResponse.StatusCode);
            Console.WriteLine(error?.Message);
        }
        else
        {
            // если запрос завершился успешно, получаем объект Person
            var idPerson = await getResponse.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"Name: {idPerson?.Name}   Age: {idPerson?.Age}");
        }

        var content = new StringContent("Tom");
        using var dataResponse = await httpClient.PostAsync($"{ServerAddress}/data", content);
        responseText = await dataResponse.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        // отправляемый объект 
        var tom = new Person { Name = "Tom", Age = 38 };
        // отправляем запрос
        using var createResponse = await httpClient.PostAsJsonAsync($"{ServerAddress}/create", tom);
        var person = await createResponse.Content.ReadFromJsonAsync<Person>();
        Console.WriteLine($"{person?.Id} - {person?.Name}");
    }
}

// для успешного ответа
class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

// для ошибок
record Error(string Message);
