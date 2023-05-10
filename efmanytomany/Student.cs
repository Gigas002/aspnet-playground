public class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public List<Course> Courses { get; set; } = new();

    // public Dictionary<Student, Relation> Relations { get; set; } = new();
    public List<StudentRelations> Relations { get; set; } = new();

    public Dictionary<Student, Relation> GetRelations() =>
        Relations.ToDictionary(sr => sr.RelatedStudent, sr => sr.Relation);
}
