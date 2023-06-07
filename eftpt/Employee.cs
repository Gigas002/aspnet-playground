using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Employees")]
public class Employee : User
{
    public int Salary { get; set; }
}
