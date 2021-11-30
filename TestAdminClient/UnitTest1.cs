using DataLayer.Backend;
using Microsoft.EntityFrameworkCore;
using TestCustomerClient;
using Xunit;


namespace TestAdminClient
{
    public class AdminLoginTestSuite
    {
        private DbContextOptions options;
        private UserBackend userBackend;

        public AdminLoginTestSuite()
        {
            var optionBuilder = new DbContextOptionsBuilder();

            optionBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=FoodRescueTestDb");

            options = optionBuilder.Options;
            userBackend = new UserBackend(optionBuilder.Options);

            var database = new Database(options);
            database.Recreate();
            database.SeedTestData();
        }
        [Fact]
        void LoginTest()
        {

        }

    }

}
