using Microsoft.EntityFrameworkCore;

var userById = EF.CompileQuery((ApplicationContext db, int id) =>
                    db.Users.Include(c => c.Company).FirstOrDefault(c => c.Id == id));

var usersByNameAndAge = EF.CompileQuery((ApplicationContext db, string name, int age) =>
           db.Users.Include(c => c.Company)
                   .Where(u => EF.Functions.Like(u.Name!, name) && u.Age > age));

// добавляем данные для тестирования
using (var db = new ApplicationContext())
{
    var microsoft = new Company { Name = "Microsoft" };
    var google = new Company { Name = "Google" };
    db.Companies.AddRange(microsoft, google);

    var tom = new User { Name = "Tom", Age = 36, Company = microsoft };
    var bob = new User { Name = "Bob", Age = 39, Company = google };
    var alice = new User { Name = "Alice", Age = 28, Company = microsoft };
    var kate = new User { Name = "Kate", Age = 25, Company = google };
    var tomas = new User { Name = "Tomas", Age = 30, Company = microsoft };
    var tomek = new User { Name = "Tomek", Age = 42, Company = google };

    db.Users.AddRange(tom, bob, alice, kate, tomas, tomek);
    db.SaveChanges();
}
using (var db = new ApplicationContext())
{
    var user = userById(db, 1);
    if (user != null) Console.WriteLine($"{user.Name} - {user.Age} \n");

    var users = usersByNameAndAge(db, "%Tom%", 30).ToList();
    foreach (var u in users)
        Console.WriteLine($"{u.Name} - {u.Age}");
}
