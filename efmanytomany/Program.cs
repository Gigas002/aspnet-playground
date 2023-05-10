using Microsoft.EntityFrameworkCore;

using (ApplicationContext db = new ApplicationContext())
{
    // // пересоздадим базу данных
    // db.Database.EnsureDeleted();
    // db.Database.EnsureCreated();
 
    // создание и добавление моделей
    Student tom = new Student { Name = "Tom" };
    Student alice = new Student { Name = "Alice" };
    Student bob = new Student { Name = "Bob" };
    db.Students.AddRange(tom, alice, bob);
 
    Course algorithms = new Course { Name = "Алгоритмы" };
    Course basics = new Course { Name = "Основы программирования" };
    db.Courses.AddRange(algorithms, basics);
 
    // добавляем к студентам курсы
    tom.Courses.Add(algorithms);
    tom.Courses.Add(basics);

    tom.Relations.Add(new() { Student = tom, RelatedStudent = bob, Relation = Relation.Friend});
    bob.Relations.Add(new() { Student = bob, RelatedStudent = alice, Relation = Relation.Enemy});

    alice.Courses.Add(algorithms);
    bob.Courses.Add(basics);
                 
    db.SaveChanges();
}

using (ApplicationContext db = new ApplicationContext())
{
    var courses = db.Courses.Include(c => c.Students).ThenInclude(c => c.Relations).ToList();
    // выводим все курсы
    foreach (var c in courses)
    {
        Console.WriteLine($"Course: {c.Name}");
        // выводим всех студентов для данного кура
        foreach (Student s in c.Students)
        {
            Console.WriteLine($"Name: {s.Name}");
            foreach (var rel in s.Relations)
            {
                Console.WriteLine($"Relto: {rel.RelatedStudent.Name} reltype: {rel.Relation}");
            }
        }
        Console.WriteLine("-------------------");
    }
}

using (ApplicationContext db = new ApplicationContext())
{
    Student? alice = db.Students.Include(s => s.Courses).FirstOrDefault(s => s.Name == "Alice");
    Course? algorithms = db.Courses.FirstOrDefault(c => c.Name == "Алгоритмы");
    Course? basics = db.Courses.FirstOrDefault(c => c.Name == "Основы программирования");
    if (alice != null && algorithms != null && basics != null)
    {
        // удаление курса у студента
        alice.Courses.Remove(algorithms);
        // добавление нового курса студенту
        alice.Courses.Add(basics);
        db.SaveChanges();
    }
}
