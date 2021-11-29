using Xunit;
using DataLayer.Backend;
using DataLayer.Model;
using DataLayer.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace TestRestaurantClient
{
    public class TestRestaurantClientTestsuit
    {

        private RestaurantBackend _restaurantBackend;

        public TestRestaurantClientTestsuit()
        {
            _restaurantBackend = new RestaurantBackend();
        }

        // Test för att lägga till en lunchlåda

        [Fact]
        public void TestAddLunchBox()
        {
            //Arrange

            int asianDeli = 1;
            var asianDeliConversion = _restaurantBackend.FindObjectById(asianDeli);
            var unsoldLunchboxes = _restaurantBackend.GetUnsoldLB(asianDeliConversion);

            //ACT

            _restaurantBackend.AddLunchBox("PoyasTestBox", "vego", 40.00m, asianDeliConversion);

            // Assert

            Assert.Contains(unsoldLunchboxes, a => a.DishName == "PoyasTestBox");

        }

        // Test för att

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