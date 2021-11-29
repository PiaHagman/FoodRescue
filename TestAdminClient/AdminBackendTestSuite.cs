using DataLayer.Backend;
using DataLayer.Data;
using System.Linq;
using Xunit;

namespace TestAdminClient
{
    public class AdminBackendTestSuite
    {
        private AdminBackend _AdminBackend;

        public AdminBackendTestSuite()
        {
            _AdminBackend = new AdminBackend();  // Anslutningen st�ngs inte ner efter testet. Inga problem enligt Bj�rn.

            //Skapar om databasen inf�r varje test
            //AdminBackend admin = new AdminBackend();
            //admin.CreateAndSeedDb();

            _AdminBackend.CreateAndSeedDb(); // Fungerar denna? Den �r lite kortare.

        }

        [Fact]
        public void Test_AddRestaurant()
        {


            _AdminBackend.AddRestaurant("Sv�rtnamn", "S�tila", "03013456456");

            using var ctx = new FoodRescueDbContext();
            var query = ctx.Restaurants
                .Where(c => c.Name == "Sv�rtnamn")
                .ToList();

            var userRecord = query.Single(); // Kraschar med mening n�r det blir mer �n en anv�ndare (tror jag)
            
            Assert.Equal(1, query.Count());


            //var user = LoadMyUser(id); // load the entity
            //Assert.AreEqual("Mr", user.Title); // test your properties
            //Assert.AreEqual("Joe", user.Firstname);
            //Assert.AreEqual("Bloggs", user.Lastname);
        }
        // AdminBackend.Database.EnsureDeleted(); Skulle ha kunnat radera databasen

    }
}