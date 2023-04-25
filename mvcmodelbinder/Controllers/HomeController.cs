using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers;

public class HomeController : Controller
{
    static List<Event> events = new List<Event>();

    public IActionResult Index()
    {
        return View(events);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Event myEvent)
    {
        // myEvent.Id = Guid.NewGuid().ToString();
        events.Add(myEvent);
        return RedirectToAction("Index");
    }
}
