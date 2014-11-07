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
    public virtual ICollection<Photo> Photos { get; set; }
    public override string ToString() { return JsonConvert.SerializeObject(this); }
}

public class Photo
{
    public int PhotoId { get; set; }
    public int UserId { get; set; }
    public byte[] PhotoData { get; set; }
    public override string ToString() { return JsonConvert.SerializeObject(this); }
}

public class MyDb : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
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
