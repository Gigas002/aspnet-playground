using System.Net.Sockets;
using System.Text;
 
using var udpServer = new UdpClient(5555);

Console.WriteLine("UDP-сервер запущен...");
 
// получаем данные
var result = await udpServer.ReceiveAsync();

// предположим, что отправлена строка, преобразуем байты в строку
var message = Encoding.UTF8.GetString(result.Buffer);
 
Console.WriteLine($"Получено {result.Buffer.Length} байт");
Console.WriteLine($"Удаленный адрес: {result.RemoteEndPoint}");
Console.WriteLine(message);
