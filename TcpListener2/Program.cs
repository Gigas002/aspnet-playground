using System.Net;
using System.Net.Sockets;
using System.Text;
 
var tcpListener = new TcpListener(IPAddress.Any, 8888);
var words = new Dictionary<string, string>()
{
    {"red", "красный" },
    {"blue", "синий" },
    {"green", "зеленый" }
};
try
{
    tcpListener.Start();    // запускаем сервер
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");
 
    while (true)
    {
        // получаем подключение в виде TcpClient
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();
        // получаем объект NetworkStream для взаимодействия с клиентом
        var stream = tcpClient.GetStream();
        // буфер для входящих данных
        var response = new List<byte> ();
        int bytesRead = 10;
        while(true)
        {
            // считываем данные до конечного символа
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                // добавляем в буфер
                response.Add((byte)bytesRead);
            }
            var word = Encoding.UTF8.GetString(response.ToArray());
 
            // если прислан маркер окончания взаимодействия,
            // выходим из цикла и завершаем взаимодействие с клиентом
            if (word == "END") break;
 
            Console.WriteLine($"Запрошен перевод слова {word}");
            // находим слово в словаре и отправляем обратно клиенту
            if (!words.TryGetValue(word, out var translation)) translation = "не найдено в словаре";
            // добавляем символ окончания сообщения 
            translation += '\n';
            // отправляем перевод слова из словаря
            await stream.WriteAsync(Encoding.UTF8.GetBytes(translation));
            response.Clear();
        }
    }
}
finally
{
    tcpListener.Stop();
}
