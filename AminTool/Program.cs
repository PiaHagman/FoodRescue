using DataLayer.Backend;
using Microsoft.EntityFrameworkCore;



var optionBuilder = new DbContextOptionsBuilder();
optionBuilder.UseSqlServer(
    @"server=(localdb)\MSSQLLocalDB;database=FoodRescueLiveDb");

var database = new Database(optionBuilder.Options);

database.Recreate();
database.SeedTestData();
Console.WriteLine("Database initialized");
Thread.Sleep(1000);
