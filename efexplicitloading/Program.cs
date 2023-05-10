using Microsoft.EntityFrameworkCore;
 
using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
 
    // добавляем начальные данные
    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };
    db.Companies.AddRange(microsoft, google);
 
 
    User tom = new User { Name = "Tom", Company = microsoft };
    User bob = new User { Name = "Bob", Company = google };
    User alice = new User { Name = "Alice", Company = microsoft };
    User kate = new User { Name = "Kate", Company = google };
    db.Users.AddRange(tom, bob, alice, kate);
 
    db.SaveChanges();
}
using (ApplicationContext db = new ApplicationContext())
{
    Company? company = db.Companies.FirstOrDefault();
    if (company != null)
    {
        db.Users.Where(u => u.CompanyId == company.Id).Load();
 
        Console.WriteLine($"Company: {company.Name}");
        foreach (var u in company.Users)
            Console.WriteLine($"User: {u.Name}");
    }
}
