using System.Net.Sockets;

public static class Program
{
    public static void Print(string message)
    {
        if (OperatingSystem.IsWindows())
        {
            var position = Console.GetCursorPosition();
            int left = position.Left;
            int top = position.Top;

            Console.MoveBufferArea(0, top, left, 1, 0, top + 1);

            Console.SetCursorPosition(0, top);
            Console.WriteLine(message);
            Console.SetCursorPosition(left, top + 1);
        }
        else Console.WriteLine(message);
    }

    public static async Task ReceiveMessageAsync(StreamReader reader)
    {
        while (true)
        {
            try
            {
                var message = await reader.ReadLineAsync();

                if (string.IsNullOrEmpty(message)) continue;

                Print(message);
            }
            catch
            {
                break;
            }
        }
    }

    public static async Task SendMessageAsync(StreamWriter writer, string userName)
    {
        await writer.WriteLineAsync(userName);
        await writer.FlushAsync();

        Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");

        while (true)
        {
            string? message = Console.ReadLine();
            await writer.WriteLineAsync(message);
            await writer.FlushAsync();
        }
    }

    public static async Task Main()
    {
        var host = "127.0.0.1";
        var port = 8888;

        using var client = new TcpClient();

        Console.Write("Введите свое имя: ");
        var userName = Console.ReadLine();

        Console.WriteLine($"Добро пожаловать, {userName}");

        StreamReader? Reader = null;
        StreamWriter? Writer = null;

        try
        {
            client.Connect(host, port);

            Reader = new StreamReader(client.GetStream());
            Writer = new StreamWriter(client.GetStream());

            if (Writer is null || Reader is null) return;

            Task.Run(() => ReceiveMessageAsync(Reader));

            await SendMessageAsync(Writer, userName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Writer?.Close();
        Reader?.Close();
    }
}
