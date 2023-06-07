class Program
{
    static HttpClient httpClient = new HttpClient();

    static async Task Main()
    {
        // определяем данные запроса
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://www.google.com");

        // получаем ответ
        // using HttpResponseMessage response = await httpClient.SendAsync(request);
        using HttpResponseMessage response = await httpClient.GetAsync("https://www.google.com");


        // просматриваем данные ответа
        // статус
        Console.WriteLine($"Status: {response.StatusCode}\n");
        //заголовки
        Console.WriteLine("Headers");
        foreach (var header in response.Headers)
        {
            Console.Write($"{header.Key}:");
            foreach (var headerValue in header.Value)
            {
                Console.WriteLine(headerValue);
            }
        }
        // содержимое ответа
        Console.WriteLine("\nContent");
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
    }
}