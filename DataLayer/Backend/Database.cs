using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Backend
{
    public class Database
    {
        private DbContextOptions options;

        public Database(DbContextOptions options)
        {
            this.options = options;
        }

        public void SeedTestData()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new() {Name = "Asian Deli", Address = "Kungsportsavenyn 3, 414 03 Göteborg", PhoneNumber = "031-653798"},
                new() {Name = "LeRum", Address = "Brobacken 7, 44330 Lerum", PhoneNumber = "0302-16797"},
                new() {Name = "Swedish Taco", Address = "Bagges Torg 19, 44330 Lerum", PhoneNumber = "0302-10059"}
            };

            List<User> users = new List<User>
            {
                new() {DateRegistered = DateTime.Parse("2021-01-10")},
                new() {DateRegistered = DateTime.Parse("2021-08-15")},
                new() {DateRegistered = new DateTime(2021, 09, 01)},    //Annat sätt att skriva på
                new() {DateRegistered = DateTime.Today.AddDays(-3)},                //Ännu annat sätt
                new() {DateRegistered = DateTime.Today},
                new() {DateRegistered = DateTime.Today},
                new() {DateRegistered = DateTime.Today}

            };

            List<UserPersonalInfo> personalInfo = new List<UserPersonalInfo>
            {
                new() {FullName = "Pia Hagman", Username = "Pia.Hagman", Password = "HelloWorld1", Email = "hagman.pia@gmail.com", User = users[0]},
                new() {FullName = "Kim Björnsen", Username = "Kim.Bjornsen", Password = "HelloWorld1", Email = "bjornsen.kim@gmail.com", User = users[1]},
                new() {FullName = "Johan Fahlgren", Username = "Johan.Fahlgren", Password = "HelloWorld1", Email = "johan.fahlgren@gmail.com", User = users[2]},
                new() {FullName = "Sofiia Löf", Username = "Sofiia.Lof", Password = "HelloWorld1", Email = "lof.sofiia@gmail.com", User = users[3]},
                new() {FullName = "Joakim Engström", Username = "Joakim.Engstrom", Password = "HelloWorld1", Email = "engstrom.joakim@gmail.com", User = users[4]},
                new() {FullName = "Anna Märta", Username = "Anna.Marta", Password = "HelloWorld1", Email = "marta.anna@gmail.com", User = users[5]},
                new() {FullName = "Poya Ghahremani", Username = "Poya.Ghahremani", Password = "HelloWorld1", Email = "ghahremani.poya@gmail.com", User = users[6]}
            };

            List<LunchBox> lunchBoxes = new List<LunchBox>
            {
                new() {DishName = "Veggie Spring Rolls", DishType = "Vego", Price = 75, Restaurant = restaurants[0]},
                new() {DishName = "Red Curry Beef", DishType = "Kött", Price = 90, Restaurant = restaurants[0]},
                new() {DishName = "Fried Shrimp", DishType = "Fisk", Price = 100, Restaurant = restaurants[0], ConsumeBefore = DateTime.Today.AddDays(-10)},
                new() {DishName = "Gös med palsternacksmos", DishType = "Fisk", Price = 85, Restaurant = restaurants[1]},
                new() {DishName = "Köttbullar och brunsås", DishType = "Kött", Price = 75, Restaurant = restaurants[1]},
                new() {DishName = "Zucchinibiffar med svampsås", DishType = "Vego", Price = 95, Restaurant = restaurants[1]},
                new() {DishName = "Taco Bowl Original", DishType = "Kött", Price = 85, Restaurant = restaurants[2]},
                new() {DishName = "Aspen Bowl", DishType = "Kött", Price = 90, Restaurant = restaurants[2]},
                new() {DishName = "Taco Roll", DishType = "Vego", Price = 75, Restaurant = restaurants[2]},
                new() {DishName = "Green Curry", DishType = "Vego", Price = 75, Restaurant = restaurants[0], ConsumeBefore = DateTime.Today.AddDays(-10)},
                new() {DishName = "Ärtsoppa och pannkaka", DishType = "Kött", Price = 75, Restaurant = restaurants[1], ConsumeBefore = DateTime.Today.AddDays(-5)},
                new() {DishName = "Kid bowl", DishType = "Kött", Price = 50, Restaurant = restaurants[2], ConsumeBefore = DateTime.Today.AddDays(-3)}
            };

            List<ItemSale> itemSales = new List<ItemSale>
            {
                new () {SalesDate = DateTime.Today.AddDays(-40), User = users[0], LunchBoxes = new[] {lunchBoxes[0], lunchBoxes[1]}},     //Två köp på en order     
                new () {SalesDate = DateTime.Today.AddDays(-40), User = users[0], LunchBoxes = new[]{lunchBoxes[2]}},
                new () {SalesDate = DateTime.Today.AddDays(-15), User = users[1], LunchBoxes = new[]{lunchBoxes[3]}},
                new () {SalesDate = DateTime.Today.AddDays(-10), User = users[4], LunchBoxes = new[]{lunchBoxes[4]}},
                new () {SalesDate = DateTime.Today.AddDays(-7), User = users[2], LunchBoxes = new[]{lunchBoxes[7]}}
            };

            using var ctx = new FoodRescueDbContext(options);
            ctx.AddRange(users);

            ctx.AddRange(personalInfo);

            ctx.AddRange(restaurants);

            ctx.AddRange(lunchBoxes);

            ctx.AddRange(itemSales);

            ctx.SaveChanges();
        }

        public void SeedLiveData()
        {
            List<User> users = new List<User>
            {
                new() {DateRegistered = DateTime.Today}
            };

            List<UserPersonalInfo> personalInfo = new List<UserPersonalInfo>
            {
                new()
                {
                    FullName = "Admin", Username = "admin", Password = "f4gtwrh4yh", Email = "admin@admin.se", User = users[0]
                }
            };

            using var ctx = new FoodRescueDbContext(options);
            ctx.AddRange(users);
            ctx.AddRange(personalInfo);
            ctx.SaveChanges();
        }

        public void Recreate()
        {
            using var ctx = new FoodRescueDbContext(options);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }
    }

    
}
