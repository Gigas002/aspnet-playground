using Microsoft.EntityFrameworkCore;

using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
     
    Role adminRole = new Role { Name = "admin" };
    Role userRole = new Role { Name = "user" };
    User user1 = new User { Name = "Tom", Age = 17, Role = userRole };
    User user2 = new User { Name = "Bob", Age = 18, Role = userRole };
    User user3 = new User { Name = "Alice", Age = 19, Role = adminRole };
    User user4 = new User { Name = "Sam", Age = 20, Role = adminRole };
    User user5 = new User { Name = "Pip", Age = 16, Role = adminRole };
     
    db.Roles.AddRange(userRole, adminRole);
    db.Users.AddRange(user1, user2, user3, user4, user5);
    db.SaveChanges();
}
 
using (ApplicationContext db = new ApplicationContext() { RoleId = 2 })
{
    var users = db.Users.Include(u => u.Role).ToList();
    foreach (User user in users)
        Console.WriteLine($"Name: {user.Name}  Age: {user.Age}  Role: {user.Role?.Name}");
}
