using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Components;

public class Header : ViewComponent
{
    public async Task<string> InvokeAsync()
    {
        using (StreamReader reader = new StreamReader("Files/header.txt"))
            return await reader.ReadToEndAsync();
    }
}