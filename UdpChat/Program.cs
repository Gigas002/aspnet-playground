using System.Net;
using System.Net.Sockets;
using System.Text;
 
IPAddress localAddress = IPAddress.Parse("127.0.0.1");

Console.Write("Введите свое имя: ");
var username = Console.ReadLine();

Console.Write("Введите порт для приема сообщений: ");
if (!int.TryParse(Console.ReadLine(), out var localPort)) return;

Console.Write("Введите порт для отправки сообщений: ");
if (!int.TryParse(Console.ReadLine(), out var remotePort)) return;

Console.WriteLine();
 
// запускаем получение сообщений
Task.Run(ReceiveMessageAsync);

// запускаем ввод и отправку сообщений
await SendMessageAsync();
 
// отправка сообщений в группу
async Task SendMessageAsync()
{
    using var client = new UdpClient();

    Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");
    
    while (true)
    {
        var message = Console.ReadLine(); 

        if (string.IsNullOrWhiteSpace(message)) break;

        message = $"{username}: {message}";

        var data = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(data, new IPEndPoint(localAddress, remotePort));
    }
}

async Task ReceiveMessageAsync()
{
    using var client = new UdpClient(localPort);

    while (true)
    {
        var result = await client.ReceiveAsync();

        var message = Encoding.UTF8.GetString(result.Buffer);
        Console.WriteLine(message);
    }
}
