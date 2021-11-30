using System;
using System.Collections.Generic;
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


    }
}
