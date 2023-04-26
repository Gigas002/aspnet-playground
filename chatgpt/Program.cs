using System.Net.Http.Json;
using System.Text.Json;

namespace ChatGPT;

public class Program
{
    private const string ModelName = "gpt-3.5-turbo";

    private const string SecretPath = "../secrets.txt";

    public static async Task Main()
    {
        // токен из личного кабинета
        var secret = File.ReadAllText(SecretPath);

        // адрес api для взаимодействия с чат-ботом
        var endpoint = "https://api.openai.com/v1/chat/completions";

        // набор соообщений диалога с чат-ботом
        var messages = new List<Message>();

        // HttpClient для отправки сообщений
        var httpClient = new HttpClient();

        // устанавливаем отправляемый в запросе токен
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {secret}");

        // init json serializing options for snake_case
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        while (true)
        {
            // ввод сообщения пользователя
            Console.Write("User message: ");

            var content = Console.ReadLine();

            // если введенное сообщение имеет длину меньше 1 символа
            // то выходим из цикла и завершаем программу
            if (content is not { Length: > 0 }) break;

            // формируем отправляемое сообщение
            // добавляем сообщение в список сообщений
            messages.Add(new Message("user", content));

            // формируем отправляемые данные
            var requestData = new Request(ModelName, messages);

            // отправляем запрос
            using var response = await httpClient.PostAsJsonAsync(endpoint, requestData, options);

            // если произошла ошибка, выводим сообщение об ошибке на консоль
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");

                break;
            }

            // получаем данные ответа
            var responseData = await response.Content.ReadFromJsonAsync<ResponseData>(options);

            var choices = responseData?.Choices ?? new List<Choice>();

            if (choices.Count == 0)
            {
                Console.WriteLine("No choices were returned by the API");

                continue;
            }

            var choice = choices[0];
            var responseMessage = choice.Message;

            // добавляем полученное сообщение в список сообщений
            messages.Add(responseMessage);
            var responseText = responseMessage.Content.Trim();

            Console.WriteLine($"ChatGPT: {responseText}");
        }
    }
}
