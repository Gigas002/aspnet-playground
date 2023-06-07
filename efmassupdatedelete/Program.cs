using Microsoft.EntityFrameworkCore;

using (var db = new ApplicationContext())
{
    if (db.Database.CanConnect())
    {
        // remove all users with only sql, without getting the objects first
        db.Users.ExecuteDelete();
    }
}

using (var db = new ApplicationContext())
{
    var user1 = new User { Name = "Tom", Age = 17 };
    var user2 = new User { Name = "Bob", Age = 18 };
    var user3 = new User { Name = "Alice", Age = 19 };
    var user4 = new User { Name = "Sam", Age = 20 };

    db.Users.AddRange(user1, user2, user3, user4);
    db.SaveChanges();
}

using (var db = new ApplicationContext())
{
    // execute sql update without getting the elements from db to modify them
    db.Users.ExecuteUpdate(s => s.SetProperty(u => u.Age, u => u.Age + 1));

    // обновляем только объекты, у которых имя Tom
    // db.Users.Where(u => u.Name == "Tom")
    //     .ExecuteUpdate(s => s
    //             .SetProperty(u => u.Age, u => u.Age + 1)    // Age = Age + 1
    //             .SetProperty(u => u.Name, u => "Tomas"));      // Name = "Tomas
}

using (var db = new ApplicationContext())
{
    var users = db.Users;

    foreach (var user in users)
    {
        Console.WriteLine($"Name: {user.Name}, Age: {user.Age}");
    }
}
