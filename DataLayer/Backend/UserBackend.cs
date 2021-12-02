using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Backend
{
    public class UserBackend
    {
        private DbContextOptions options;
        public UserBackend(DbContextOptions options)
        {
            this.options = options;
        }


        #region TryLogin()

        public User TryLogin(string username, string password)
        {
            //kolla om username finns
            using var ctx = new FoodRescueDbContext(options);
            var query = ctx
                .Users
                .Include(u => u.PersonalInfo)
                .Where(u => u.PersonalInfo.Username == username);

            var user = query.FirstOrDefault(); //.singleOrDefault skulle fungera bättre om vi inte hade ett unik constraint 

            if (user == null) return null;

            if (user.PersonalInfo.Password == password) return user;
            

            return null;

        }
        #endregion

        #region TryAdminLogin

        public User TryAdminLogin(string username, string password)
        {
            //kolla om username finns
            using var ctx = new FoodRescueDbContext(options);
            var query = ctx
                .Users
                .Include(u => u.PersonalInfo)
                .Where(u => u.PersonalInfo.Username == username);

            var user = query.FirstOrDefault(); //.singleOrDefault skulle fungera bättre om vi inte hade ett unik constraint 

            if (user == null) return null;

            if (user.PersonalInfo.Password == password && user.Admin == true) return user;


            return null;

        }

        #endregion

        #region GetAvailableLunchBoxes()

        public List<LunchBox> GetAvailableLunchBoxes(string dishType)
        {
            using var ctx = new FoodRescueDbContext(options);
            var lunchBoxes = ctx.LunchBoxes
                .Include(lb => lb.Restaurant)
                .Where(lb => lb.DishType == dishType && lb.ItemSale == null)
                .OrderBy(lb => lb.Price);

            return lunchBoxes.ToList();

        }
        #endregion

        #region BuyThisLunchBox()
        public LunchBox BuyThisLunchBox(int lunchBoxId, User user)
        {
            using var ctx = new FoodRescueDbContext(options);
            var availableLunchBoxes = ctx.LunchBoxes
                .Include(lb => lb.Restaurant)
                .Where(lb => lb.ItemSale == null && lb.Id==lunchBoxId);

            var lunchBox = availableLunchBoxes.FirstOrDefault();

            user = ctx.Users.Find(user.Id);

            bool lunchBoxExists = lunchBox != null;
            
            if (lunchBoxExists)
            {
                ctx.ItemSales.Add(new ItemSale { SalesDate = DateTime.Today, LunchBoxes = new[] { lunchBox }, User = user });
                ctx.SaveChanges();

                return lunchBox;
            }
            return null;
        }

        #endregion

        #region GetBoughtBoxes()
        public List<ItemSale> GetBoughtBoxes(User user)
        {
            using var ctx = new FoodRescueDbContext(options);

            List<ItemSale> boughtBoxes = 
                ctx.ItemSales
                    .Include(i => i.LunchBoxes)
                    .ThenInclude(lb => lb.Restaurant)
                    .Where(lb => lb.User.Id == user.Id).ToList();

            return boughtBoxes;
        }
        #endregion

        #region CreateUser()

        public void CreateUser(string fullname, string username, string password, string email)
        {
            using var ctx = new FoodRescueDbContext(options);
            
            ctx.PersonalInfo.Add(new()
                {FullName = fullname, Username = username, Password = password, Email = email, User = new User()});

            ctx.SaveChanges();
            
        }

        #endregion

        public void CreateAdminUser(string fullname, string username, string password, string email)
        {
            using var ctx = new FoodRescueDbContext(options);
            var adminUser = ctx.Users.Add(new() { Admin = true });
            ctx.PersonalInfo.Add(new()
                { FullName = fullname, Username = username, Password = password, Email = email, User = adminUser.Entity });

            ctx.SaveChanges();

        }



    }
}
