using System.Net;

class Program
{
    public const string ServerAddress = "http://localhost:5170";
    static HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        // адрес сервера
        var uri = new Uri(ServerAddress);

        var response = await httpClient.GetAsync(uri);
        var cookies = new CookieContainer();
        // получаем из запроса все элементы с заголовком Set-Cookie
        foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
            // добавляем заголовки кук в CookieContainer
            cookies.SetCookies(uri, cookieHeader);

        // получение всех куки
        foreach (Cookie cookie in cookies.GetCookies(uri))
            Console.WriteLine($"{cookie.Name}: {cookie.Value}");

        // получение отдельных куки
        // получаем куку "email"
        var email = cookies.GetCookies(uri).FirstOrDefault(c => c.Name == "email");
        Console.WriteLine($"Электронный адрес: {email?.Value}");


        uri = new Uri($"{ServerAddress}/recieve");
        cookies = new CookieContainer();
        // устанавливаем куки name и email
        cookies.Add(uri, new Cookie("name", "Bob"));
        cookies.Add(uri, new Cookie("email", "bob@localhost.com"));
        // устанавливаем заголовок cookie
        httpClient.DefaultRequestHeaders.Add("cookie", cookies.GetCookieHeader(uri));

        response = await httpClient.GetAsync(uri);
        var responseText = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseText);

        response.Dispose();
    }
}
