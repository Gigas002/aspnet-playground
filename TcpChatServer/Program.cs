namespace TcpChatServer;

public static class Program
{
    public static async Task Main()
    {
        var server = new Server();

        await server.ListenAsync();
    }
}
