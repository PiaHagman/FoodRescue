using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Backend
{
    public class Login
    {
        private DbContextOptions options;

        public Login(DbContextOptions options)
        {
            this.options = options;
        }

        public UserPersonalInfo User(string username, string password)
        {
            using var ctx = new FoodRescueDbContext(options);

            var query = ctx.Users
                .Where(user => user. == username);
            return user;
        }


    }
}
