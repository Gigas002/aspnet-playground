using System.Net;
using System.Net.Sockets;

namespace TcpChatServer;

public class Server
{
    private TcpListener _tcpListener = new TcpListener(IPAddress.Any, 8888);

    private List<Client> _clients = new List<Client>();

    protected internal void RemoveConnection(string id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        
        if (client != null) _clients.Remove(client);

        client?.Close();
    }

    protected internal async Task ListenAsync()
    {
        try
        {
            _tcpListener.Start();
            
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                var tcpClient = await _tcpListener.AcceptTcpClientAsync();

                var client = new Client(tcpClient, this);
                _clients.Add(client);
                
                Task.Run(client.ProcessAsync);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Disconnect();
        }
    }

    protected internal async Task BroadcastMessageAsync(string message, string id)
    {
        foreach (var client in _clients)
        {
            if (client.Id != id)
            {
                await client.Writer.WriteLineAsync(message);
                await client.Writer.FlushAsync();
            }
        }
    }

    protected internal void Disconnect()
    {
        foreach (var client in _clients)
        {
            client.Close();
        }

        _tcpListener.Stop();
    }
}
