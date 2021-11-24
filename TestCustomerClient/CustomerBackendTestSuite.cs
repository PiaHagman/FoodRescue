using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using DataLayer.Backend;
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
            //Resistrerar ny kund
            UserBackend.CreateUser("Jon Krantz", "jon.krantz", "HelloWorld1",
                 "krantz.jon@gmail.com");

            //Provar att logga in den nya kunden, samt några felaktiga inloggningsförsök
            var loggedInUser = UserBackend.TryLogin("jon.krantz", "HelloWorld1");
            var userIsNull = UserBackend.TryLogin("karin.holm", "HelloWorld1");
            var existingUsernameWrongPassword = UserBackend.TryLogin("jon.krantz", "Hello");

            Assert.True(loggedInUser != null);
            Assert.True(userIsNull == null);
            Assert.True(existingUsernameWrongPassword == null);
        }

        [Fact]
        void TryBuyLunchBoxTest()
        {
            //Logga in en befintlig användare som ska användas för köp
            var user = UserBackend.TryLogin("pia.hagman", "HelloWorld1");

            //objektet skickas in i UserBackend för att användas vid köpet
            UserBackend activeUser = new UserBackend(user);

            //Köp en lunchbox genom att ange dess id (id plockas från tabell-data)
            bool canBuyThisLunchBox = activeUser.BuyThisLunchBox(1);
            Assert.True(canBuyThisLunchBox);

            //Försök köpa samma lunchbox igen
            bool cantBuyThisLunchBox = activeUser.BuyThisLunchBox(1);
            Assert.False(cantBuyThisLunchBox);
            
            //Prova att köpa en lunchbox med ett id som inte finns
            bool LunchBoxDoesntExist = activeUser.BuyThisLunchBox(20);
            Assert.False(LunchBoxDoesntExist);
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