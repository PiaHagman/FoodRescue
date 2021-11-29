using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using DataLayer.Backend;
using DataLayer.Data;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestCustomerClient
{
    public class CustomerBackendTestSuite
    {
        //private UserBackend _userBackend;
        private DbContextOptions options;
        private UserBackend userBackend;

        public CustomerBackendTestSuite()
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
        void RegisterAndLoginNewUserTest()
        {
            
            //Resistrerar ny kund
            userBackend.CreateUser("Jon Krantz", "jon.krantz", "HelloWorld1",
                 "krantz.jon@gmail.com");

            //Provar att logga in den nya kunden, samt några felaktiga inloggningsförsök
            var loggedInUser = userBackend.TryLogin("jon.krantz", "HelloWorld1");
            var userIsNull = userBackend.TryLogin("karin.holm", "HelloWorld1");
            var existingUsernameWrongPassword = userBackend.TryLogin("jon.krantz", "Hello");

            Assert.NotNull(loggedInUser);
            Assert.Null(userIsNull);
            Assert.Null(existingUsernameWrongPassword);
            Assert.Equal("jon.krantz", loggedInUser.PersonalInfo.Username);
            Assert.Equal("Jon Krantz", loggedInUser.PersonalInfo.FullName);
            Assert.Equal("HelloWorld1", loggedInUser.PersonalInfo.Password);
            Assert.Equal("krantz.jon@gmail.com", loggedInUser.PersonalInfo.Email);
       

        }

        [Fact]
        void TryBuyLunchBoxTest()
        {
            
            //Logga in en befintlig användare som ska användas för köp
            var user = userBackend.TryLogin("pia.hagman", "HelloWorld1");

            using var ctx = new FoodRescueDbContext(options);
            var lunchBox = ctx.LunchBoxes.Find(1);

            //Köp den lunchbox vi plockat ut från databasen
            if (lunchBox != null)
            {
                LunchBox lunchBoxObject = userBackend.BuyThisLunchBox(lunchBox.Id, user);

                Assert.NotNull(lunchBoxObject);
                Assert.Equal(lunchBox.DishName, lunchBoxObject.DishName);
           

            //Försök köpa samma lunchbox igen, få null tillbaka
            var lunchBoxIsNull = userBackend.BuyThisLunchBox(lunchBox.Id, user);
            Assert.Null(lunchBoxIsNull);
            }

            //Prova att köpa en lunchbox med ett id som inte finns, få null tillbaka
            var lunchBoxDoesntExist = userBackend.BuyThisLunchBox(20, user);
            Assert.Null(lunchBoxDoesntExist);
        }

        [Fact]

        void GetBoughtBoxesTest()
        {
            //Logga in en befintlig användare som ska användas för köp
            var user = userBackend.TryLogin("pia.hagman", "HelloWorld1");

            var boughtBoxes= userBackend.GetBoughtBoxes(user);

            //Kontrollera att de ItemSales som tillhör pia.hagman återfinns i listan och andra inte
            Assert.Contains(boughtBoxes, i => i.Id == 1 || i.Id == 2);
            Assert.DoesNotContain(boughtBoxes, i => i.Id == 4);

        }

     

    }
}