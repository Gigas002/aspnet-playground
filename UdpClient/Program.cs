using System.Net;
using System.Net.Sockets;
using System.Text;
 
using var udpClient = new UdpClient();
 
// отправляемые данные
string message = "Hello METANIT.COM";

// преобразуем в массив байтов
byte[] data = Encoding.UTF8.GetBytes(message);

// определяем конечную точку для отправки данных
IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);

// отправляем данные
int bytes = await udpClient.SendAsync(data, remotePoint);

Console.WriteLine($"Отправлено {bytes} байт");
