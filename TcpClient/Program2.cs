using System.Net.Sockets;
using System.Text;

var port = 80;
var url = "www.google.com";

using var tcpClient = new TcpClient();
await tcpClient.ConnectAsync(url, port);
var stream = tcpClient.GetStream();

var message = $"GET / HTTP/1.1\r\nHost: {url}\r\nConnection: Close\r\n\r\n";

var messageBytes = Encoding.UTF8.GetBytes(message);
await stream.WriteAsync(messageBytes);

int bytes;

// буфер для получения данных
var responseBytes = new byte[512];

var builder = new StringBuilder();

do
{
    bytes = await stream.ReadAsync(responseBytes);
    var responsePart = Encoding.UTF8.GetString(responseBytes, 0, bytes);
        builder.Append(responsePart);
}
while (bytes > 0); 
 
Console.WriteLine(builder);
