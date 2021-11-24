using System.Runtime.CompilerServices;
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
            UserBackend.CreateUser("Jon Krantz", "jon.krantz", "HelloWorld1",
                 "krantz.jon@gmail.com");

            //Prova att logga in
            var loggedInUser = UserBackend.TryLogin("jon.krantz", "HelloWorld1");
            var userIsNull = UserBackend.TryLogin("karin.holm", "HelloWorld1");
            var existingUsernameWrongPaswword = UserBackend.TryLogin("jon.krantz", "Hello");

            Assert.True(loggedInUser != null);
            Assert.True(userIsNull == null);
            Assert.True(existingUsernameWrongPaswword == null);
        }

        

    }
}