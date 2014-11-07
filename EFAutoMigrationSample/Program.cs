using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFAutoMigrationSample.Migrations;
using Newtonsoft.Json;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    [Required]
    public string Note { get; set; }
    public override string ToString() { return JsonConvert.SerializeObject(this); }
}

public class MyDb : DbContext
{
    public DbSet<User> Users { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyDb, Configuration>());

        var db = new MyDb();

        if (db.Users.Any() == false)
        {
            db.Users.Add(new User
            {
                Name = "Jhon"
            });
            db.SaveChanges();
        }

        db.Users.ToList().ForEach(Console.WriteLine);
    }
}
