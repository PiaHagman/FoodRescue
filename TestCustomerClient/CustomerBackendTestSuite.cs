using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using DataLayer.Backend;
using DataLayer.Data;
using DataLayer.Model;
using Xunit;

namespace TestCustomerClient
{
    public class CustomerBackendTestSuite
    {
        private UserBackend _userBackend;

        public CustomerBackendTestSuite()
        {
            _userBackend = new UserBackend();

            //Skapar om databasen inför varje test
            AdminBackend admin = new AdminBackend();
            admin.CreateAndSeedDb();
        }


        [Fact]
        void RegisterAndLoginNewUserTest()
        {

            //TODO Kolla mot databas istället

            //Resistrerar ny kund
            UserBackend.CreateUser("Jon Krantz", "jon.krantz", "HelloWorld1",
                 "krantz.jon@gmail.com");

            //Provar att logga in den nya kunden, samt några felaktiga inloggningsförsök
            var loggedInUser = UserBackend.TryLogin("jon.krantz", "HelloWorld1");
            var userIsNull = UserBackend.TryLogin("karin.holm", "HelloWorld1");
            var existingUsernameWrongPassword = UserBackend.TryLogin("jon.krantz", "Hello");

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
            var user = UserBackend.TryLogin("pia.hagman", "HelloWorld1");

            //objektet skickas in i UserBackend för att användas vid köpet
            UserBackend activeUser = new UserBackend(user);

            using var ctx = new FoodRescueDbContext();
            var lunchBox = ctx.LunchBoxes.Find(1);

            //Köp den lunchbox vi plockat ut från databasen
            LunchBox lunchBoxObject = activeUser.BuyThisLunchBox(lunchBox.Id);

            Assert.NotNull(lunchBoxObject);
            Assert.Equal(lunchBox.DishName, lunchBoxObject.DishName);

            //Försök köpa samma lunchbox igen, få null tillbaka
            var luncBoxIsNull = activeUser.BuyThisLunchBox(lunchBox.Id);
            Assert.Null(luncBoxIsNull);

            //Prova att köpa en lunchbox med ett id som inte finns, få null tillbaka
            var lunchBoxDoesntExist = activeUser.BuyThisLunchBox(20);
            Assert.Null(lunchBoxDoesntExist);
        }

        [Fact]

        void GetBoughtBoxesTest()
        {
            //Logga in en befintlig användare som ska användas för köp
            var user = UserBackend.TryLogin("pia.hagman", "HelloWorld1");

            //objektet skickas in i UserBackend för att användas vid köpet
            UserBackend activeUser = new UserBackend(user);

            var boughtBoxes= activeUser.GetBoughtBoxes();

            //Kontrollera att de ItemSales som tillhör pia.hagman återfinns i listan och andra inte
            Assert.Contains(boughtBoxes, i => i.Id == 1 || i.Id == 2);
            Assert.DoesNotContain(boughtBoxes, i => i.Id == 4);

        }

     

    }
}