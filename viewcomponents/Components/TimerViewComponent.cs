using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace MvcApp.Components;

// // [ViewComponent]
// public class TimerViewComponent : ViewComponent
// {
//     public string Invoke()
//     {

//         return $"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}";
//     }
// }


// [ViewComponent]
// public class Timer
// {
//     public string Invoke(bool includeSeconds)
//     {
//         if (includeSeconds)
//             return $"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}";
//         else
//             return $"Текущее время: {DateTime.Now.ToString("hh:mm")}";
//     }
// }

// [ViewComponent]
// public class Timer
// {
//     ITimeService timeService;
//     public Timer(ITimeService service)
//     {
//         timeService = service;
//     }
//     public string Invoke()
//     {
//         return $"Текущее время: {timeService.GetTime()}";
//     }
// }

public class Timer : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        string time = $"Текущее время: {DateTime.Now.ToString("HH:mm:ss")}";
        return Content(time);
        // return new ContentViewComponentResult(time);
    }

    // public IViewComponentResult Invoke()
    // {
    //     return new HtmlContentViewComponentResult(
    //         new HtmlString($"<p>Текущее время:<b>{DateTime.Now.ToString("HH:mm:ss")}</b></p>")
    //     );
    // }
}
