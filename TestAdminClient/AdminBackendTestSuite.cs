using DataLayer.Backend;
using DataLayer.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestAdminClient
{
    public class AdminBackendTestSuite
    {
        
            //private UserBackend _userBackend;
            private DbContextOptions options;
            private AdminBackend adminBackend;

            public AdminBackendTestSuite()
            {

                var optionBuilder = new DbContextOptionsBuilder(options);

                optionBuilder.UseSqlServer(
                    @"server=(localdb)\MSSQLLocalDB;database=FoodRescueTestDb");

                options = optionBuilder.Options;
                adminBackend = new AdminBackend(optionBuilder.Options);

                var database = new Database(options);
                database.Recreate();
                database.SeedTestData();
            }




            /*public AdminBackendTestSuite()
            {
                _AdminBackend = new AdminBackend();  // Anslutningen stängs inte ner efter testet. Inga problem enligt Björn.

                //Skapar om databasen inför varje test
                //AdminBackend admin = new AdminBackend();
                //admin.CreateAndSeedDb();

                _AdminBackend.CreateAndSeedDb(); // Fungerar denna? Den är lite kortare.

            }*/

            [Fact]
        public void Test_AddRestaurant()
        {


            adminBackend.AddRestaurant("Svårtnamn", "Sätila", "03013456456");

            using var ctx = new FoodRescueDbContext(options);
            var query = ctx.Restaurants
                .Where(c => c.Name == "Svårtnamn")
                .ToList();

            var userRecord = query.Single(); // Kraschar med mening när det blir mer än en användare (tror jag)
            
            Assert.Equal(1, query.Count());


            //var user = LoadMyUser(id); // load the entity
            //Assert.AreEqual("Mr", user.Title); // test your properties
            //Assert.AreEqual("Joe", user.Firstname);
            //Assert.AreEqual("Bloggs", user.Lastname);
        }
        // AdminBackend.Database.EnsureDeleted(); Skulle ha kunnat radera databasen

    }
}