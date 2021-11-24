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

            //Skapar om databasen inf�r varje test
            AdminBackend admin = new AdminBackend();
            admin.CreateAndSeedDb();
        }


        [Fact]
        void RegisterAndLoginNewUserTest()
        {
            //Resistrerar ny kund
            UserBackend.CreateUser("Jon Krantz", "jon.krantz", "HelloWorld1",
                 "krantz.jon@gmail.com");

            //Provar att logga in den nya kunden, samt n�gra felaktiga inloggningsf�rs�k
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
            //Logga in en befintlig anv�ndare som ska anv�ndas f�r k�p
            var user = UserBackend.TryLogin("pia.hagman", "HelloWorld1");

            //objektet skickas in i UserBackend f�r att anv�ndas vid k�pet
            UserBackend activeUser = new UserBackend(user);

            //K�p en lunchbox genom att ange dess id (id plockas fr�n tabell-data)
            bool canBuyThisLunchBox = activeUser.BuyThisLunchBox(1);
            Assert.True(canBuyThisLunchBox);

            //F�rs�k k�pa samma lunchbox igen
            bool cantBuyThisLunchBox = activeUser.BuyThisLunchBox(1);
            Assert.False(cantBuyThisLunchBox);
            
            //Prova att k�pa en lunchbox med ett id som inte finns
            bool LunchBoxDoesntExist = activeUser.BuyThisLunchBox(20);
            Assert.False(LunchBoxDoesntExist);
        }


     

    }
}