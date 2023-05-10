// Добавление
using (ApplicationContext db = new ApplicationContext())
{
    User tom = new User { Name = "Tom", Age = 33 };
    User alice = new User { Name = "Alice", Age = 26 };
 
    Console.WriteLine($"Id перед добавлением в контекст {tom.Id}");    // Id = 0

    db.Users.AddRange(tom, alice);
    db.SaveChanges();

    Console.WriteLine($"Id после добавления в базу данных {tom.Id}");  // Id = 1
}
 
// получение
using (ApplicationContext db = new ApplicationContext())
{
    // получаем объекты из бд и выводим на консоль
    var users = db.Users.ToList();
    Console.WriteLine("Данные после добавления:");
    foreach (User u in users)
    {
        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
    }
}
