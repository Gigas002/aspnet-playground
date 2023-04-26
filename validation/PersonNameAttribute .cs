using System.ComponentModel.DataAnnotations;

namespace MvcApp;

public class PersonNameAttribute : ValidationAttribute
{
    //массив для хранения допустимых имен
    string[] _names;

    public PersonNameAttribute(string[] names)
    {
        _names = names;
    }
    public override bool IsValid(object? value)
    {
        return value != null && _names.Contains(value);
    }
}
