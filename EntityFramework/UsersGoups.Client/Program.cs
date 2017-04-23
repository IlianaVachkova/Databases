// Task 11

using System;
using System.Linq;
using UsersGroups.Data;

class Program
{
    static void Main()
    {
        using (var db = new UsersGroupsEntities())
        {
            if (db.Groups.Where(x => x.Name == "Admin").Count() == 0)
            {
                var adminGroup = new Group { Name = "Admin" };
                db.Groups.Add(adminGroup);
            }

            var user = new User 
            {
                Name = "Nakov" , 
                GroupId = db.Groups.FirstOrDefault(x => x.Name == "Admin").GroupId
            };
            db.Users.Add(user);
            db.SaveChanges();
            Console.WriteLine("User {0} - {1} added", user.Name, user.Group.Name);
        }
    }
}