using Xunit;
using DataLayer.Backend;
using DataLayer.Model;
using DataLayer.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace TestRestaurantClient
{
    public class TestRestaurantClientTestsuit
    {

        private DbContextOptions options;
        private RestaurantBackend _restaurantBackend;

        public TestRestaurantClientTestsuit()
        {

            var optionBuilder = new DbContextOptionsBuilder();

            optionBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=FoodRescueTestDb");

            options = optionBuilder.Options;
            _restaurantBackend = new RestaurantBackend(optionBuilder.Options);

            var database = new Database(options);
            database.Recreate();
            database.SeedTestData();
        }
        // Test f�r att l�gga till en lunchl�da

        [Fact]
        public void TestAddLunchBox()
        {
            //Arrange

            int asianDeli = 1;
            var asianDeliConversion = _restaurantBackend.FindObjectById(asianDeli);
            
            //ACT

            _restaurantBackend.AddLunchBox("PoyasTestBox", "vego", 40.00m, asianDeliConversion);
            var unsoldLunchboxes = _restaurantBackend.GetUnsoldLB(asianDeliConversion);

            // Assert

            Assert.Contains(unsoldLunchboxes, a => a.DishName == "PoyasTestBox");

        }

        // Test f�r att

        [Fact]

        public void TestUnsoldLunchboxes()
        {

            //Arrange
            int AsianDeli = 1;
            var AsianDeliConversion = _restaurantBackend.FindObjectById(AsianDeli);
            var unsoldLunchboxes = _restaurantBackend.GetUnsoldLB(AsianDeliConversion);

            Assert.NotEmpty(unsoldLunchboxes);

        }
        

    }
}