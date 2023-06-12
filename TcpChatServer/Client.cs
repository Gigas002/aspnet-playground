using System.Net.Sockets;

namespace TcpChatServer;

public class Client
{
    protected internal string Id { get; } = Guid.NewGuid().ToString();

    protected internal StreamWriter Writer { get; }

    protected internal StreamReader Reader { get; }

    private TcpClient _tcpClient;

    private Server _server;

    public Client(TcpClient tcpClient, Server server)
    {
        _tcpClient = tcpClient;
        _server = server;

        var stream = _tcpClient.GetStream();
        Reader = new StreamReader(stream);
        Writer = new StreamWriter(stream);
    }

    public async Task ProcessAsync()
    {
        try
        {
            string? userName = await Reader.ReadLineAsync();
            string? message = $"{userName} вошел в чат";

            await _server.BroadcastMessageAsync(message, Id);
            Console.WriteLine(message);

            while (true)
            {
                try
                {
                    message = await Reader.ReadLineAsync();

                    if (message == null) continue;
                    
                    message = $"{userName}: {message}";
                    Console.WriteLine(message);
                    
                    await _server.BroadcastMessageAsync(message, Id);
                }
                catch
                {
                    message = $"{userName} покинул чат";
                    Console.WriteLine(message);

                    await _server.BroadcastMessageAsync(message, Id);
                    
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            _server.RemoveConnection(Id);
        }
    }

    protected internal void Close()
    {
        Writer.Close();
        Reader.Close();
        _tcpClient.Close();
    }
}
