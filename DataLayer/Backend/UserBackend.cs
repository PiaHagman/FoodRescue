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

        private User _user;

        public UserBackend(){}

        public UserBackend(User user)
        {
            _user = user;
        }

        #region TryLogin()

        public static User TryLogin(string username, string password)
        {
            //kolla om username finns
            using var ctx = new FoodRescueDbContext();
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

        #region GetAvailableLunchBoxes()

        public List<LunchBox> GetAvailableLunchBoxes(string dishtype)
        {
            using var ctx = new FoodRescueDbContext();
            var lunchBoxes = ctx.LunchBoxes
                .Include(lb => lb.Restaurant)
                .Where(lb => lb.DishType == dishtype && lb.ItemSale == null)
                .OrderBy(lb => lb.Price);

            return lunchBoxes.ToList();

        }
        #endregion

        #region BuyThisLunchBox()
        public LunchBox BuyThisLunchBox(int lunchboxid)
        {
            using var ctx = new FoodRescueDbContext();
            var availablelunchBoxes = ctx.LunchBoxes
                .Include(lb => lb.Restaurant)
                .Where(lb => lb.ItemSale == null && lb.Id==lunchboxid);

            var lunchBox = availablelunchBoxes.FirstOrDefault();

            _user = ctx.Users.Find(_user.Id);

            bool lunchBoxExists = lunchBox != null;
            
            if (lunchBoxExists)
            {
                ctx.ItemSales.Add(new ItemSale { SalesDate = DateTime.Today, LunchBoxes = new[] { lunchBox }, User = _user });
                ctx.SaveChanges();

                return lunchBox;
            }
            return null;
        }

        #endregion

        #region GetBoughtBoxes()
        public List<ItemSale> GetBoughtBoxes()
        {
            using var ctx = new FoodRescueDbContext();

            List<ItemSale> boughtBoxes = 
                ctx.ItemSales
                    .Include(i => i.LunchBoxes)
                    .ThenInclude(lb => lb.Restaurant)
                    .Where(lb => lb.User.Id == _user.Id).ToList();

            return boughtBoxes;
        }
        #endregion

        #region CreateUser()

        public static void CreateUser(string fullname, string username, string password, string email)
        {
            using var ctx = new FoodRescueDbContext();

            //var query = ctx.Users;

            ctx.PersonalInfo.Add(new()
                {FullName = fullname, Username = username, Password = password, Email = email, User = new User()});

            ctx.SaveChanges();
            
        }

        #endregion



        
    }
}
