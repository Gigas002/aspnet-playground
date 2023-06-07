using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

using (var db = new ApplicationContext())
{
    if (db.Database.CanConnect())
    {
        db.Users.ExecuteDelete();
        db.Companies.ExecuteDelete();
    }
}

using (var db = new ApplicationContext())
{
    Company microsoft = new Company { Name = "Microsoft"};
    Company google = new Company { Name = "Google"};
    db.Companies.AddRange(microsoft, google);
 
    User tom = new User { Name = "Tom", Age = 36, Company = microsoft };
    User bob = new User { Name = "Bob", Age = 39, Company = google };
    User alice = new User { Name = "Alice", Age = 28, Company = microsoft };
    User kate = new User { Name = "Kate", Age = 25, Company = google };
    User tomas = new User { Name = "Tomas", Age = 22, Company = microsoft };
    User tomek = new User { Name = "Tomek", Age = 42, Company = google };
 
    db.Users.AddRange(tom, bob, alice, kate, tomas, tomek);
    db.SaveChanges();
}

// get all objects from Companies table with raw sql query

using (var db = new ApplicationContext())
{
    // get data

    var comps = db.Companies.FromSqlRaw("SELECT * FROM Companies").ToList();

    // var comps = db.Companies.FromSqlRaw("SELECT * FROM Companies").OrderBy(x=>x.Name).ToList();
    // will be:
    // SELECT "c"."Id", "c"."Name"
    // FROM (
    //    SELECT * FROM Companies
    // ) AS "c"
    // ORDER BY "c"."Name"

    // SqliteParameter param = new SqliteParameter("@name", "%Tom%");
    // var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE @name", param).ToList();

    var name = "%Tom%";
    var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name LIKE {0}", name).ToList();

    foreach (var company in comps)
        Console.WriteLine(company.Name);

    // insert data

    string newComp = "Apple";
    int numberOfRowInserted = db.Database.ExecuteSqlRaw("INSERT INTO Companies (Name) VALUES ({0})", newComp);

    // update data
    string appleInc = "Apple Inc.";
    string apple = "Apple";
    int numberOfRowUpdated = db.Database.ExecuteSqlRaw("UPDATE Companies SET Name={0} WHERE Name={1}", appleInc, apple);

    // delete data
    int numberOfRowDeleted = db.Database.ExecuteSqlRaw("DELETE FROM Companies WHERE Name={0}", appleInc);
}

// interpolated (safe)

using (ApplicationContext db = new ApplicationContext())
{
    var name = "%Tom%";
    var age = 30;
    var users = db.Users.FromSqlInterpolated($"SELECT * FROM Users WHERE Name LIKE {name} AND Age > {age}").ToList();
    foreach (var user in users)
        Console.WriteLine(user.Name);

    string jetbrains = "JetBrains";
    db.Database.ExecuteSqlInterpolated($"INSERT INTO Companies (Name) VALUES ({jetbrains})");
 
    foreach (var comp in db.Companies.ToList())
        Console.WriteLine(comp.Name);
}
