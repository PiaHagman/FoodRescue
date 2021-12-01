using DataLayer.Backend;
using Microsoft.EntityFrameworkCore;
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
            var loginTestCorrect = userBackend.TryLogin("pia.hagman", "HelloWorld1");
            Assert.Equal("Pia Hagman", loginTestCorrect.PersonalInfo.FullName);

            var loginTestWrong = userBackend.TryLogin("gustav.alverus", "HelloWorld1");
            Assert.Null(loginTestWrong);

        }

    }

}
