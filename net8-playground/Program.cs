using System.Globalization;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{

    public static void Main()
    {
        CultureInfo ci = null;// new("ru-RU");

        if (ci is null) Console.WriteLine("ci is null");
        else Console.WriteLine(ci.NativeName);

        Taras panis = new("hello taras");
        Console.WriteLine(panis.Panis);

        var u8str = "this is utf8"u8;
        Console.WriteLine(Encoding.UTF8.GetString(u8str));

        var rawString = """
        asdas
        sd
        <>?{}P!*^$&Y!(G^*) 
        """;
        Console.WriteLine(rawString.ToString());

        // ReadBigFile("t.md");
        ReadBigFile("1.docx");

        // using Measurement = (string Units, int Distance);
        // Measurement.Units = "m";
        // Console.WriteLine(Measurement.Units);
    }

    public static void ReadBigFile(string path, string outPath = "big.json")
    {
        BigJson json = new();
        json.Text = File.ReadAllText(path);

        string jsonString = JsonSerializer.Serialize(json);
        File.WriteAllText(outPath, jsonString);
    }
}

public class BigJson
{
    public string Text { get; set; }
}

public class Taras(string panis)
{
    public string Panis => panis;
}

public class Person
{
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
}
