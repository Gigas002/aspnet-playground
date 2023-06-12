using System.Net.Sockets;
using System.Text;
 
using TcpClient tcpClient = new TcpClient();
await tcpClient.ConnectAsync("127.0.0.1", 8888);
 
// слова для отправки для получения перевода
var words = new string[] { "red", "yellow", "blue" };
// получаем NetworkStream для взаимодействия с сервером
var stream = tcpClient.GetStream();
 
// буфер для входящих данных
var response = new List<byte>();
int bytesRead = 10; // для считывания байтов из потока
foreach (var word in words)
{
    // считыванием строку в массив байт
    // при отправке добавляем маркер завершения сообщения - \n
    byte[] data = Encoding.UTF8.GetBytes(word + '\n');
    // отправляем данные
    await stream.WriteAsync(data);
 
    // считываем данные до конечного символа
    while ((bytesRead = stream.ReadByte()) != '\n')
    {
        // добавляем в буфер
        response.Add((byte)bytesRead);
    }
    var translation = Encoding.UTF8.GetString(response.ToArray());
    Console.WriteLine($"Слово {word}: {translation}");
    response.Clear();
}
 
// отправляем маркер завершения подключения - END
await stream.WriteAsync(Encoding.UTF8.GetBytes("END\n"));
Console.WriteLine("Все сообщения отправлены");
