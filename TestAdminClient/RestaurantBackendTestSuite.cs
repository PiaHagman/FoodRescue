using DataLayer.Backend;
using DataLayer.Data;
using System.Linq;
using Xunit;

namespace TestAdminClient
{
    public class RestaurantBackendTestSuite
    {
        private AdminBackend _AdminBackend;

        public RestaurantBackendTestSuite()
        {
            _AdminBackend = new AdminBackend();

            //Skapar om databasen inför varje test
            AdminBackend admin = new AdminBackend();
            admin.CreateAndSeedDb();
        }

        [Fact]
        public void Test_AddRestaurant()
        {

            
                _AdminBackend.AddRestaurant("Svårtnamn", "Sätila", "03013456456");

                using var ctx = new FoodRescueDbContext();
                    var query = ctx.Restaurants
                        .Where(c => c.Name == "Svårtnamn")
                        .ToList();

                Assert.Equal(1, query.Count());


                //var user = LoadMyUser(id); // load the entity
                //Assert.AreEqual("Mr", user.Title); // test your properties
                //Assert.AreEqual("Joe", user.Firstname);
                //Assert.AreEqual("Bloggs", user.Lastname);
            


        }
    }
}