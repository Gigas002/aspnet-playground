using System.Net.Http.Headers;

class Program
{
    public const string ServerAddress = "http://localhost:5286";
    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        // отправляемые данные
        string filePath = "~/Documents/forest.webp";

        // send stream

        using var fileStream = File.OpenRead(filePath);
        // создаем объект HttpContent
        var content = new StreamContent(fileStream);
        // отправляем запрос
        var response = await httpClient.PostAsync($"{ServerAddress}/data", content);
        // получаем ответ
        string responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        // send any data as bytes

        // отправляемые данные
        string message = "Hello METANIT.COM";
        // считываем строку в массив байтов 
        byte[] messageToBytes = System.Text.Encoding.UTF8.GetBytes(message);
        // формируем отправляемое содержимое
        var byteContent = new ByteArrayContent(messageToBytes);
        // отправляем запрос
        response = await httpClient.PostAsync($"{ServerAddress}/bata", byteContent);
        // получаем ответ
        responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        // send file

        // создаем MultipartFormDataContent
        using var multipartFormContent = new MultipartFormDataContent();

        // добавляем обычные данные
        multipartFormContent.Add(new StringContent("Tom"), name: "username");
        multipartFormContent.Add(new StringContent("tom@localhost.com"), name: "email");

        // Загружаем отправляемый файл
        var fileStreamContent = new StreamContent(File.OpenRead(filePath));
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        multipartFormContent.Add(fileStreamContent, name: "file", fileName: "forest.jpg");
 
        // Отправляем данные
        response = await httpClient.PostAsync($"{ServerAddress}/upload", multipartFormContent);
        // считываем ответ
        responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        response.Dispose();
    }
}
