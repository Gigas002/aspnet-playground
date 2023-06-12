using System.Net;
using System.Text;

var listener = new HttpListener();

// установка адресов прослушки
listener.Prefixes.Add("http://127.0.0.1:8888/connection/");

// начинаем прослушивать входящие подключения
listener.Start();

Console.WriteLine("Сервер запущен. Ожидание подключений...");

// получаем контекст
var context = await listener.GetContextAsync();

// получаем данные запроса
var request = context.Request;

Console.WriteLine($"адрес приложения: {request.LocalEndPoint}");
Console.WriteLine($"адрес клиента: {request.RemoteEndPoint}");
Console.WriteLine(request.RawUrl);
Console.WriteLine($"Запрошен адрес: {request.Url}");
Console.WriteLine("Заголовки запроса:");

foreach (string item in request.Headers.Keys)
{
    Console.WriteLine($"{item}:{request.Headers[item]}");
}

// получаем объект для установки ответа
var response = context.Response;

var buffer = Encoding.UTF8.GetBytes("Hello METANIT");

// получаем поток ответа и пишем в него ответ
response.ContentLength64 = buffer.Length;

using var output = response.OutputStream;

// отправляем данные
await output.WriteAsync(buffer);
await output.FlushAsync();

// останавливаем сервер
listener.Stop();
