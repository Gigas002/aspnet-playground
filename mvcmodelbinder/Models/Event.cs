namespace MvcApp.Models;

public class Event
{
    public string? Id { get; set; }
    public string? Name { get; set; }       // название события
    public DateTime EventDate { get; set; } // дата и время событие
}
