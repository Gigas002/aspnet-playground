using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Managers")]
public class Manager : User
{
    public string? Departament { get; set; }
}
