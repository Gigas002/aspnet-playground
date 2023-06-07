using System.Net;
using System.Net.Http.Json;
class Program
{
    public const string ServerAddress = "http://localhost:5293";
    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        // id первого объекта 
        int id = 1;
        var response = await httpClient.GetAsync($"{ServerAddress}/api/users/{id}");
        // если объект на сервере найден, то есть статусный код равен 404
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Error? error = await response.Content.ReadFromJsonAsync<Error>();
            Console.WriteLine(error?.Message);
        }
        else if (response.StatusCode == HttpStatusCode.OK)
        {
            // считываем ответ
            var perdson = await response.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"{perdson?.Id} - {perdson?.Name}");
        }

        // отправляемый объект
        var mike = new Person { Name = "Mike", Age = 31 };
        response = await httpClient.PostAsJsonAsync($"{ServerAddress}/api/users/", mike);
        // считываем ответ и десериализуем данные в объект Person
        Person? person = await response.Content.ReadFromJsonAsync<Person>();
        Console.WriteLine($"{person?.Id} - {person?.Name}");

        // id изменяемого объекта
        id = 1;
        // отправляемый объект
        var tom = new Person { Id = id, Name = "Tomas", Age = 38 };
        response = await httpClient.PutAsJsonAsync($"{ServerAddress}/api/users/", tom);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // если возникла ошибка, считываем сообщение об ошибке
            Error? error = await response.Content.ReadFromJsonAsync<Error>();
            Console.WriteLine(error?.Message);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            // десериализуем ответ в объект Person
            var perdson = await response.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"{perdson?.Id} - {perdson?.Name} ({perdson?.Age})");
        }

        // id удаляемого объекта
        id = 1;
 
        response = await httpClient.DeleteAsync($"{ServerAddress}/api/users/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // если возникла ошибка, считываем сообщение об ошибке
            Error? error = await response.Content.ReadFromJsonAsync<Error>();
            Console.WriteLine(error?.Message);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            // десериализуем ответ в объект Person
            var perdson = await response.Content.ReadFromJsonAsync<Person>();
            Console.WriteLine($"{perdson?.Id} - {perdson?.Name} ({perdson?.Age})");
        }

        // данные для отправки в виде объекта IEnumerable<KeyValuePair<string, string>>
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            ["name"]= "Tom",
            ["email"]= "tom@localhost.com",
            ["age"] = "38"
        };
        // создаем объект HttpContent
        HttpContent contentForm = new FormUrlEncodedContent(data);
        // отправляем запрос
        response = await httpClient.PostAsync($"{ServerAddress}/data", contentForm);
        // получаем ответ
        string responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        response.Dispose();
    }
}
record Error(string Message);
class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
