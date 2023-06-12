using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program
{
    public static async Task Main()
    {
        var server = new Server();
        var serverTask = server.StartAsync();
        
        using var client = new Client();
        var clientTask = client.StartAsync();

        await Task.WhenAll(serverTask, clientTask);
    }
}

public class Server : INetworkElement
{
    public IPAddress IPAddress => IPAddress.Any;
    public int Port => 8888;
    public TcpListener TcpListener { get; init; }

    public Server()
    {
        TcpListener = new TcpListener(IPAddress, Port);
    }

    public Task StartAsync()
    {
        return Task.Run(async () =>
        {
            TcpListener.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений... ");

            while (true)
            {
                var tcpClient = await TcpListener.AcceptTcpClientAsync();

                // создаем новую задачу для обслуживания нового клиента
                await Task.Run(async () => await ProcessClientAsync(tcpClient));
            }
        });
    }

    public void Stop()
    {
        TcpListener.Stop();
    }

    public async Task ProcessClientAsync(TcpClient tcpClient)
    {
        var words = new Dictionary<string, string>()
        {
            {"red", "красный" },
            {"blue", "синий" },
            {"green", "зеленый" },
        };

        var stream = tcpClient.GetStream();
        var response = new List<byte>();
        int bytesRead = 10;

        while (true)
        {
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                response.Add((byte)bytesRead);
            }

            var word = Encoding.UTF8.GetString(response.ToArray());

            if (word == "END")
            {
                Console.WriteLine($"Client {tcpClient.Client.RemoteEndPoint} sent END confirmation");

                break;
            }

            Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил перевод слова {word}");

            if (!words.TryGetValue(word, out var translation)) translation = "не найдено в словаре";

            translation += '\n';

            await stream.WriteAsync(Encoding.UTF8.GetBytes(translation));

            response.Clear();
        }

        tcpClient.Close();
    }
}

public class Client : INetworkElement, IDisposable
{
    public IPAddress IPAddress => IPAddress.Parse("127.0.0.1");
    public int Port => 8888;

    public TcpClient TcpClient { get; init; }

    public Client()
    {
        TcpClient = new TcpClient();
    }

    public Task StartAsync()
    {
        return Task.Run(async () =>
        {
            var words = new string[] { "red", "yellow", "blue", "green" };

            await TcpClient.ConnectAsync(IPAddress, Port);
            var stream = TcpClient.GetStream();

            var response = new List<byte>();
            int bytesRead = 10;

            foreach (var word in words)
            {
                var data = Encoding.UTF8.GetBytes(word + '\n');
                await stream.WriteAsync(data);

                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    response.Add((byte)bytesRead);
                }

                var translation = Encoding.UTF8.GetString(response.ToArray());

                Console.WriteLine($"Слово {word}: {translation}");
                response.Clear();

                // wait so several clients can connect
                await Task.Delay(2000);
            }

            // отправляем маркер завершения подключения - END
            await stream.WriteAsync(Encoding.UTF8.GetBytes("END\n"));
        });
    }

    public void Dispose()
    {
        TcpClient.Dispose();
    }
}

public interface INetworkElement
{
    public IPAddress IPAddress { get; }

    public int Port { get; }
}
