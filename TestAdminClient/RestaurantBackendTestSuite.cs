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

            //Skapar om databasen inf�r varje test
            AdminBackend admin = new AdminBackend();
            admin.CreateAndSeedDb();
        }

        [Fact]
        public void Test_AddRestaurant()
        {

            void AddRestaurant_test()
            {
                _AdminBackend.AddRestaurant("Preem", "S�tila", "03013456456");

                using var ctx = new FoodRescueDbContext();
                    var query = ctx.Restaurants
                        .Where(c => c.Name == "Preem")
                        .ToList()
                        ;
                Assert.NotNull(query);

                // Annars kanske jag kan g� till "Se alla kunder" och se om Preem lagts till.

                //var user = LoadMyUser(id); // load the entity
                //Assert.AreEqual("Mr", user.Title); // test your properties
                //Assert.AreEqual("Joe", user.Firstname);
                //Assert.AreEqual("Bloggs", user.Lastname);
            };


        }
    }
}