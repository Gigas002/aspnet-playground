
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("StudentId", "RelatedStudentId")]
public class StudentRelations
{
    [ForeignKey("StudentId")]
    public Student Student { get; set; } = null!;

    [ForeignKey("RelatedStudentId")]
    public Student RelatedStudent { get; set; } = null!;
    
    public Relation Relation { get; set; }
}
