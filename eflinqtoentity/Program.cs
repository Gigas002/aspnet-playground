using Microsoft.EntityFrameworkCore;

using (ApplicationContext db = new ApplicationContext())
{
    // // пересоздаем базу данных
    // db.Database.EnsureDeleted();
    // db.Database.EnsureCreated();

    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };
    db.Companies.AddRange(microsoft, google);

    User tom = new User { Name = "Tom", Age = 36, Company = microsoft };
    User bob = new User { Name = "Bob", Age = 39, Company = google };
    User alice = new User { Name = "Alice", Age = 28, Company = microsoft };
    User kate = new User { Name = "Kate", Age = 25, Company = google };

    db.Users.AddRange(tom, bob, alice, kate);
    db.SaveChanges();
}

using (ApplicationContext db = new ApplicationContext())
{
    // get all users and companies where companyid==1
    var users = db.Users.Include(p=>p.Company).Where(p=> p.CompanyId == 1);

    // same as
    users = (from u in db.Users.Include(p => p.Company)
                 where u.CompanyId == 1
                 select u);

    // get all users with name %Tom%
    users = db.Users.Where(p => EF.Functions.Like(p.Name!, "%Tom%"));

    // get element by id; same as FirstOrDefault
    User? user = db.Users.Find(3);

    // Get only Company.Name, not whole Company
    var anonymousUsers = db.Users.Select(p => new
    { 
        Name = p.Name, 
        Age = p.Age, 
        Company = p.Company!.Name 
    });

    // order sequence ascending
    users = db.Users.OrderBy(p=>p.Name);

    // join tables
    var joinUsers = db.Users.Join(db.Companies, // второй набор
        u => u.CompanyId, // свойство-селектор объекта из первого набора
        c => c.Id, // свойство-селектор объекта из второго набора
        (u, c) => new // результат
        {
            Name=u.Name, 
            Company = c.Name, 
            Age=u.Age
        });

    // group by company name
    var groups = db.Users.GroupBy(u => u.Company!.Name).Select(g => new
    {
        g.Key,
        Count = g.Count()
    });

    // union: get all users aged lesser than 30 or named Tom
    users = db.Users.Where(u => u.Age < 30)
        .Union(db.Users.Where(u=>u.Name!.Contains("Tom")));

    // intersect: get all users aged bigger than 30 and named Tom
    users = db.Users.Where(u => u.Age > 30)
        .Intersect(db.Users.Where(u=>u.Name!.Contains("Tom")));

    // select ell except...
    var selector1 = db.Users.Where(u => u.Age > 30); // 
    var selector2 = db.Users.Where(u => u.Name!.Contains("Tom"));
    users = selector1.Except(selector2);

    // is there anyone at google in table?
    bool result = db.Users.Any(u=>u.Company!.Name=="Google");

    // are everyone at google in table?
    result = db.Users.All(u=>u.Company!.Name=="Microsoft");
    
    // count users
    int number1 = db.Users.Count();
    int number2 = db.Users.Count(u => u.Name!.Contains("Tom"));

    // минимальный возраст
    int minAge = db.Users.Min(u=>u.Age);
    // максимальный возраст
    int maxAge = db.Users.Max(u=>u.Age);
    // средний возраст пользователей, которые работают в Microsoft
    double avgAge = db.Users.Where(u=>u.Company!.Name=="Microsoft")
                        .Average(p => p.Age);

    // суммарный возраст всех пользователей 
    int sum1 = db.Users.Sum(u => u.Age);
    // суммарный возраст тех, кто работает в Microsoft
    int sum2 = db.Users.Where(u=>u.Company!.Name == "Microsoft")
                        .Sum(u => u.Age);

    foreach (var u in users)
        Console.WriteLine($"{u.Name} ({u.Age}) - {u.Company?.Name}");
}

// IEnumerable vs IQueryable
using (ApplicationContext db = new ApplicationContext())
{
    // query database for all users
    IEnumerable<User> userIEnum = db.Users;
    // select from users object (not db)
    var users = userIEnum.Where(p => p.Id < 10).ToList();
 
    foreach(var user in users) Console.WriteLine(user.Name);
}

// translated to sql:
// SELECT "u"."Id", "u"."Name"
// FROM "Users" AS "u"

using (ApplicationContext db = new ApplicationContext())
{
    // query database for users with `where`
    IQueryable<User> userIQuer = db.Users;
    var users = userIQuer.Where(p => p.Id < 10).ToList();
 
    foreach(var user in users) Console.WriteLine(user.Name);
}

// translated to sql:
// SELECT "u"."Id", "u"."Name"
// FROM "Users" AS "u"
// WHERE "u"."Id" < 10
