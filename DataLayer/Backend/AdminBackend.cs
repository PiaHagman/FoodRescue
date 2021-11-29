using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DataLayer.Backend
{
    public class AdminBackend
    {
        private DbContextOptions options;
        public AdminBackend(DbContextOptions options)
        {
            this.options = options;
        }

        #region GetUsers()

        public List<User> GetUsers()
        {
            using var ctx = new FoodRescueDbContext();
            List<User> getUsers = ctx.Users
                .Include(u => u.PersonalInfo)
            using var ctx = new FoodRescueDbContext(options);
            List<User> getUsers = ctx.Users.Include(u => u.PersonalInfo)
                .ToList();      

            return getUsers;

        }
        #endregion

        #region DeleteUser()

        public bool DeleteUser(string username)
        {
            using var ctx = new FoodRescueDbContext(options);

            var query = ctx.PersonalInfo
                .Where(pi => pi.Username == username);
            
            var userPersonalInfo = query.FirstOrDefault();

            if (userPersonalInfo == null)
            {
                return false;
            }

            ctx.PersonalInfo
                .Remove(userPersonalInfo);
            
            ctx.SaveChanges();
            return true;
          
        }
        
        #endregion

        #region GetRestaurants()

        public List<Restaurant> GetRestaurants()
        {
            using var ctx = new FoodRescueDbContext(options);
            List <Restaurant> restaurantList = ctx.Restaurants.ToList();

            return restaurantList;
        }
        #endregion

        #region AddRestaurant()

        public void AddRestaurant(string name, string address, string phonenumber)
        {
            using var ctx = new FoodRescueDbContext(options);

            ctx.Restaurants.Add(new Restaurant {Name = name, Address = address, PhoneNumber = phonenumber});
            ctx.SaveChanges();
        }

        #endregion

    }



}
