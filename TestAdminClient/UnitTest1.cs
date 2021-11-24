using Xunit;

namespace TestAdminClient
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            
            void CreateAndSeedDb()
            {
                                {
                    var user = new User("Mr", "Joe", "Bloggs");
                    session.Save(user);
                    return user.UserID;
                , id => // the ID of the entity we need to load
                {
                    var user = LoadMyUser(id); // load the entity
                    Assert.AreEqual("Mr", user.Title); // test your properties
                    Assert.AreEqual("Joe", user.Firstname);
                    Assert.AreEqual("Bloggs", user.Lastname);
                }
}

        }
    }
}