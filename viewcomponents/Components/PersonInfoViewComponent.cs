using MvcApp.Models; // пространство имен класса Person

namespace MvcApp.Components;

public class PersonInfoViewComponent
{
    public string Invoke(Person Person)
    {
        return $"Name: {Person.Name}  Age: {Person.Age}";
    }
}
