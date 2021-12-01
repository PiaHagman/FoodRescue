using DataLayer.Backend;
using DataLayer.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestAdminClient
{
    public class AdminBackendTestSuite
    {
        private DbContextOptions options;
        private AdminBackend adminBackend;

        public AdminBackendTestSuite()
        {
            var optionBuilder = new DbContextOptionsBuilder();

            optionBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;database=FoodRescueTestDb");

            options = optionBuilder.Options;
            adminBackend = new AdminBackend(optionBuilder.Options);

            var database = new Database(options);
            database.Recreate();
            database.SeedTestData();
        }

        [Fact]
        public void Test_AddRestaurant()
        {
            adminBackend.AddRestaurant("Testnamn", "Test", "0301TEST");

            using var ctx = new FoodRescueDbContext(options);

            var query = ctx.Restaurants
                .Where(c => c.Name == "Testnamn")
                .ToList();

            Assert.Equal(1, query.Count());
        }
    }
}