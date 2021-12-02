using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class FoodRescueDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<LunchBox> LunchBoxes { get; set; }
        public DbSet<ItemSale> ItemSales { get; set; }
        public DbSet<UserPersonalInfo> PersonalInfo { get; set; }

   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPersonalInfo>()
                .HasIndex(e => e.Username).IsUnique();

            modelBuilder.Entity<User>()
                .Property(e => e.DateRegistered)
                .HasDefaultValue(DateTime.Today);                   //C# kod för att ange datum

            modelBuilder.Entity<LunchBox>()
                .Property(e => e.ConsumeBefore)
                .HasDefaultValueSql("DateAdd(day, 1, GetDate())");  //Sql kod för att ange datum


            modelBuilder.Entity<ItemSale>()
                .Property(e => e.SalesDate)
                .HasDefaultValue(DateTime.Today);
        }

        public FoodRescueDbContext(DbContextOptions options) : base(options){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ställer in dbcontext på annat håll via konstruktorn
            //optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=FoodRescueDb_CodeFirst");
        }

        #region TestData i listor för att seeda tabeller
        public void Seed()
        {
            

            List<Restaurant> Restaurants = new List<Restaurant>
            {
                new() {Name = "Asian Deli", Address = "Kungsportsavenyn 3, 414 03 Göteborg", PhoneNumber = "031-653798"},
                new() {Name = "LeRum", Address = "Brobacken 7, 44330 Lerum", PhoneNumber = "0302-16797"},
                new() {Name = "Swedish Taco", Address = "Bagges Torg 19, 44330 Lerum", PhoneNumber = "0302-10059"}
            };

            List<User> Users = new List<User>
            {
                new() {DateRegistered = DateTime.Parse("2021-01-10"), Admin = true},
                new() {DateRegistered = DateTime.Parse("2021-08-15")},
                new() {DateRegistered = new DateTime(2021, 09, 01)},    //Annat sätt att skriva på
                new() {DateRegistered = DateTime.Today.AddDays(-3)},                //Ännu annat sätt
                new() {DateRegistered = DateTime.Today},
                new() {DateRegistered = DateTime.Today},
                new() {DateRegistered = DateTime.Today},
              

            };

            List<UserPersonalInfo> PersonalInfo = new List<UserPersonalInfo>
            {
                new() {FullName = "Pia Hagman", Username = "Pia.Hagman", Password = "HelloWorld1", Email = "hagman.pia@gmail.com", User = Users[0]},
                new() {FullName = "Kim Björnsen", Username = "Kim.Bjornsen", Password = "HelloWorld1", Email = "bjornsen.kim@gmail.com", User = Users[1]},
                new() {FullName = "Johan Fahlgren", Username = "Johan.Fahlgren", Password = "HelloWorld1", Email = "johan.fahlgren@gmail.com", User = Users[2]},
                new() {FullName = "Sofiia Löf", Username = "Sofiia.Lof", Password = "HelloWorld1", Email = "lof.sofiia@gmail.com", User = Users[3]},
                new() {FullName = "Joakim Engström", Username = "Joakim.Engstrom", Password = "HelloWorld1", Email = "engstrom.joakim@gmail.com", User = Users[4]},
                new() {FullName = "Anna Märta", Username = "Anna.Marta", Password = "HelloWorld1", Email = "marta.anna@gmail.com", User = Users[5]},
                new() {FullName = "Poya Ghahremani", Username = "Poya.Ghahremani", Password = "HelloWorld1", Email = "ghahremani.poya@gmail.com", User = Users[6]}
            };

            List<LunchBox> LunchBoxes = new List<LunchBox>
            {
                new() {DishName = "Veggie Spring Rolls", DishType = "Vego", Price = 75, Restaurant = Restaurants[0]},
                new() {DishName = "Red Curry Beef", DishType = "Kött", Price = 90, Restaurant = Restaurants[0]},
                new() {DishName = "Fried Shrimp", DishType = "Fisk", Price = 100, Restaurant = Restaurants[0], ConsumeBefore = DateTime.Today.AddDays(-10)},
                new() {DishName = "Gös med palsternacksmos", DishType = "Fisk", Price = 85, Restaurant = Restaurants[1]},
                new() {DishName = "Köttbullar och brunsås", DishType = "Kött", Price = 75, Restaurant = Restaurants[1]},
                new() {DishName = "Zucchinibiffar med svampsås", DishType = "Vego", Price = 95, Restaurant = Restaurants[1]},
                new() {DishName = "Taco Bowl Original", DishType = "Kött", Price = 85, Restaurant = Restaurants[2]},
                new() {DishName = "Aspen Bowl", DishType = "Kött", Price = 90, Restaurant = Restaurants[2]},
                new() {DishName = "Taco Roll", DishType = "Vego", Price = 75, Restaurant = Restaurants[2]},
                new() {DishName = "Green Curry", DishType = "Vego", Price = 75, Restaurant = Restaurants[0], ConsumeBefore = DateTime.Today.AddDays(-10)},
                new() {DishName = "Ärtsoppa och pannkaka", DishType = "Kött", Price = 75, Restaurant = Restaurants[1], ConsumeBefore = DateTime.Today.AddDays(-5)},
                new() {DishName = "Kid bowl", DishType = "Kött", Price = 50, Restaurant = Restaurants[2], ConsumeBefore = DateTime.Today.AddDays(-3)}
            };

            List<ItemSale> ItemSales = new List<ItemSale>
            {
                new () {SalesDate = DateTime.Today.AddDays(-40), User = Users[0], LunchBoxes = new[] {LunchBoxes[0], LunchBoxes[1]}},     //Två köp på en order     
                new () {SalesDate = DateTime.Today.AddDays(-40), User = Users[0], LunchBoxes = new[]{LunchBoxes[2]}},
                new () {SalesDate = DateTime.Today.AddDays(-15), User = Users[1], LunchBoxes = new[]{LunchBoxes[3]}},
                new () {SalesDate = DateTime.Today.AddDays(-10), User = Users[4], LunchBoxes = new[]{LunchBoxes[4]}},
                new () {SalesDate = DateTime.Today.AddDays(-7), User = Users[2], LunchBoxes = new[]{LunchBoxes[7]}}
            };

            AddRange(Users);

            AddRange(PersonalInfo);

            AddRange(Restaurants);

            AddRange(LunchBoxes);

            AddRange(ItemSales);

            SaveChanges();

            #endregion

        }
    }
}
