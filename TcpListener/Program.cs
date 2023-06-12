using System.Net;
using System.Net.Sockets;
 
var tcpListener = new TcpListener(IPAddress.Any, 8888);
try
{
    tcpListener.Start();    // запускаем сервер
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");
 
    while (true)
    {
        // получаем подключение в виде TcpClient
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();
        Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");
    }
}
finally
{
    tcpListener.Stop(); // останавливаем сервер
}
