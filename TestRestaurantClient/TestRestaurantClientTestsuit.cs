using Xunit;
using DataLayer.Backend;
using DataLayer.Model;
using DataLayer.Data;
using System.Collections.Generic;

namespace TestRestaurantClient
{
    public class TestRestaurantClientTestsuit
    {

        private RestaurantBackend _restaurantBackend;

        public TestRestaurantClientTestsuit()
        {
            _restaurantBackend = new RestaurantBackend();

        }

        //private Resta

        [Fact]
        public void TestAddLunchBox()
        {
            //Arrange

            int AsianDeli = 1;
            var AsianDeliConversion = _restaurantBackend.FindObjectById(AsianDeli);
            var unsoldFoodboxes = _restaurantBackend.GetUnsoldLB(AsianDeliConversion);


            //ACT
             _restaurantBackend.AddLunchBox("PoyasTestBox", "vego", 40.00m, AsianDeliConversion);


            // Assert

            Assert.Contains(unsoldFoodboxes, a => a.DishName == "PoyasTestBox" && a.Restaurant == AsianDeliConversion);
            
        }
    }
}