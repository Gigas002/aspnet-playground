using System.ComponentModel.DataAnnotations;
using MvcApp.Models;

namespace MvcApp;

public class NamePasswordEqualAttribute : ValidationAttribute
{
    public NamePasswordEqualAttribute()
    {
        ErrorMessage = "Имя и пароль не должны совпадать!";
    }
    public override bool IsValid(object? value)
    {
        Person? p = value as Person;
        return p != null && p.Name != p.Password;
    }
}
