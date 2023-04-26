using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages;

// public class IndexModel : PageModel
// {
//     public string Message { get; }
//     public IndexModel()
//     {
//         Message = "Hello METANIT.COM";
//     }
//     public string PrintTime() => DateTime.Now.ToShortTimeString();
// }

// [IgnoreAntiforgeryToken]
// public class IndexModel : PageModel
// {
//     public string Message { get; private set; } = "";
//     public void OnGet()
//     {
//         Message = "Введите свое имя";
//     }
//     public void OnPost(string username)
//     {
//         Message = $"Ваше имя: {username}";
//     }
// }

// [IgnoreAntiforgeryToken]
// public class IndexModel : PageModel
// {
//     [BindProperty(SupportsGet = true, Name = "id")]
//     public string Name { get; set; } = "";

//     [BindProperty]
//     public int Age { get; set; }

//     // public string Message { get; private set; } = "";
//     // public void OnGet()
//     // {
//     //     Message = "Введите данные";
//     // }
//     // public void OnPost()
//     // {
//     //     Message = $"Имя: {Name}  Возраст: {Age}";
//     // }
// }

// public class IndexModel : PageModel
// {
//     // начальные данные - список людей
//     List<Person> people = new()
//         {
//             new Person ("Tom Smith", 23),
//             new Person ("Sam Anderson", 23),
//             new Person ("Bob Johnson", 25),
//             new Person ("Tom Anderson", 25)
//         };

//     // отображаемые данные
//     public List<Person> DisplayedPeople { get; private set; } = new();

//     // public IActionResult OnGet(string name)
//     // {
//     //     if (name != null) return Content($"Hello {name}");
//     //     return Page();
//     // }

//     public void OnGet()
//     {
//         DisplayedPeople = people;
//     }

//     public void OnGetByName(string name)
//     {
//         DisplayedPeople = people.Where(p => p.Name.Contains(name)).ToList();
//     }

//     public void OnGetByAge(int age)
//     {
//         DisplayedPeople = people.Where(p => p.Age == age).ToList();
//     }
// }

// public record class Person(string Name, int Age);

public class IndexModel : PageModel
{
    public string Message { get; private set; }
    public IndexModel(ITimeService timeService)
    {
        Message = $"Time: {timeService.Time}";
    }
}
