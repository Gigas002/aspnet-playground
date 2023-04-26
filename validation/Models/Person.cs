using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MvcApp.Models;

// [NamePasswordEqual]
// public record class Person(
//     [Required (ErrorMessage = "Не указано имя")]
//     [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
//     // [PersonName(new string[] {"Tom", "Sam", "Alice" }, ErrorMessage ="Недопустимое имя")]
//     string? Name,
//     // [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
//     [EmailAddress (ErrorMessage = "Некорректный адрес")]
//     // [Remote(action: "CheckEmail", controller: "Home", ErrorMessage ="Email уже используется")]
//     string? Email,
//     [Required]
//     string? Password,
//     // [Compare("Password", ErrorMessage = "Пароли не совпадают")]
//     // string? PasswordConfirm,
//     [Range(1, 110, ErrorMessage = "Недопустимый возраст")]
//     int Age
// );

public class Person : IValidatableObject
{
    [Display(Name = "Имя и фамилия")]
    public string? Name { get; set; }

    public string? Email { get; set; }

    [Display(Name = "Домашняя страница")]
    [UIHint("Url")]
    public string? HomePage { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public int Age { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(new ValidationResult("Введите имя!", new List<string>() { "Name" }));
        }
        if (string.IsNullOrWhiteSpace(Email))
        {
            errors.Add(new ValidationResult("Введите электронный адрес!"));
        }
        if (Age < 0 || Age > 120)
        {
            errors.Add(new ValidationResult("Недопустимый возраст!"));
        }

        return errors;
    }
}
