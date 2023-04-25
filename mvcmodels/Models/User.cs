using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

// [Bind("Name")]
public class User
{
    public int Id { get; set; }

    [BindRequired]
    public string Name { get; set; } = "";

    [BindingBehavior(BindingBehavior.Optional)]
    public int Age { get; set; }
    [BindNever]
    public bool HasRight { get; set; }
}
