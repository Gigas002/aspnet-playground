using System.Net.Sockets;
using System.Text;
 
// var port = 8888;
// var url = "127.0.0.1";

var port = 80;
var url = "www.google.com";


var response = await SocketSendReceiveAsync(url, port);
Console.WriteLine(response);
 
async Task<Socket?> ConnectSocketAsync(string url, int port)
{
    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    try
    {
        await socket.ConnectAsync(url, port);

        return socket;
    }
    catch(SocketException ex)
    {
        Console.WriteLine(ex.Message);

        socket.Close();
    }

    return null;
}
 
async Task<string> SocketSendReceiveAsync(string url, int port)
{
    using var socket = await ConnectSocketAsync(url, port);

    if (socket is null)
        return $"Не удалось установить соединение с {url}";
 
    var message = $"GET / HTTP/1.1\r\nHost: {url}\r\nConnection: Close\r\n\r\n";

    var messageBytes = Encoding.UTF8.GetBytes(message);
    await socket.SendAsync(messageBytes);

    int bytes;

    // буфер для получения данных
    var responseBytes = new byte[512];
    
    var builder = new StringBuilder();
    
    do
    {
        bytes = await socket.ReceiveAsync(responseBytes);
        var responsePart = Encoding.UTF8.GetString(responseBytes, 0, bytes);
        builder.Append(responsePart);
    }
    while (bytes > 0);
 
    return builder.ToString();
}
