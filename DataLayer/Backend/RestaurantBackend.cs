using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Backend
{
    public class RestaurantBackend
    {
        private DbContextOptions options;
        public RestaurantBackend(DbContextOptions options)
        {
            this.options = options;
        }

        //titta på alla osålda matlådor för dennes restaurang

        public List <LunchBox> GetUnsoldLB(Restaurant restaurant)
        {
            using var ctx = new FoodRescueDbContext(options);

            List <LunchBox> getUnsoldLbs = ctx.LunchBoxes.Where(lb => lb.Restaurant.Id == restaurant.Id && lb.ItemSale == null).ToList();

            return getUnsoldLbs;

        }

        #region FindObjectById()
        public Restaurant FindObjectById(int restaurantId)
        {
            using var ctx = new FoodRescueDbContext(options);
            var queryFindRestaurant = ctx
                .Restaurants
                .Where(r => r.Id == restaurantId);

            var restaurant = queryFindRestaurant.FirstOrDefault();

            if (restaurant == null)
            {
                return null;
            }
            return restaurant;
        }

        #endregion

        #region GetSoldLB()
        public List<LunchBox> GetSoldLB(Restaurant restaurant)
        {
            using var ctx = new FoodRescueDbContext(options);
            List<LunchBox> getSoldLbs = 
                ctx.LunchBoxes
                    .Where(lb => lb.Restaurant.Id == restaurant.Id && lb.ItemSale != null)
                    .ToList();

            return getSoldLbs;
        }
        #endregion

        #region AddLunchBox()
        
        public void AddLunchBox(string dishname, string dishtype, decimal price, Restaurant restaurant)
        {
            using var ctx = new FoodRescueDbContext(options);
            restaurant = ctx.Restaurants.Find(restaurant.Id);

            ctx.LunchBoxes.Add(new LunchBox { DishName=dishname, DishType=dishtype, Price=price, Restaurant=restaurant });
            ctx.SaveChanges();
        }
        #endregion

        #region GetIncomePerMonth()

        public decimal GetIncomePerMonth(int month, Restaurant restaurant)
        {
            using var ctx = new FoodRescueDbContext(options);
            restaurant = ctx.Restaurants.Find(restaurant.Id);

            var totalIncome = ctx.LunchBoxes
                .Where(lb => lb.ItemSale.SalesDate.Month == month
                             && lb.Restaurant.Id == restaurant.Id)
                .Select(lb => new
                {
                    Id = lb.Restaurant.Id,
                    Pris = lb.Price,
                    DateOfPurchase = lb.ItemSale.SalesDate

                })
                .Sum(lb => lb.Pris);

            return totalIncome;
        }

        #endregion
    
    }
}
