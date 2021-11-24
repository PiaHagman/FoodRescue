using DataLayer.Backend;
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
        public void Test1()
        {

            void AddRestaurant_test()
            {
                _AdminBackend.AddRestaurant("Preem", "Sätila", "03013456456");

                using var ctx = new FoodboxDbContext();
                    var query = ctx.Restaurants
                        .Include
                    
                    //var user = LoadMyUser(id); // load the entity
                    //Assert.AreEqual("Mr", user.Title); // test your properties
                    //Assert.AreEqual("Joe", user.Firstname);
                    //Assert.AreEqual("Bloggs", user.Lastname);
            };


        }
    }
}