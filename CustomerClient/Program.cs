using DataLayer.Backend;
using DataLayer.Model;

AdminBackend admin = new AdminBackend();
admin.CreateAndSeedDb();
Console.WriteLine("Database initialized");
Thread.Sleep(2000);

while (true)
{
    try
    {
        MainProgramLoop();
    }
    catch
    {
        Console.Clear();
        Console.WriteLine("Något gick fel. Tryck valfri tangent för att ta starta om appen.");
        Console.ReadLine();
    }
}
#region MainProgramLoop
void MainProgramLoop()
{
    Console.Clear();
    Console.WriteLine("Välkommen till Food Rescue!");
    Console.WriteLine("\nVad vill du göra? Välj 1 eller 2:");
    Console.WriteLine("\n1. Logga in. För testkund använd - användarnamn: pia.hagman, lösenord: HelloWorld1");
    Console.WriteLine("2. Registrera ny användare");

    var userChoice = Console.ReadLine();

    switch (userChoice)
    {
        case "1": //Logga in
            Login();
            break;

        case "2": //Registrera ny användare
            var loggedInUser = RegisterUser();
            ProgramLoopLoggedInCust(loggedInUser);
            break;

        default:
            Console.WriteLine("Vänligen ange 1 för att Logga in eller 2 för att Registrera nytt konto:");
            break;
    }
}
#endregion

#region Hjälpmetod SeeAndBuyLunchBox
void SeeAndBuyLunchbox(User user)
{
    UserBackend activeUser = new UserBackend(user);
    Console.Clear();
    Console.WriteLine("Lunchlådor Food Rescue:");
    Console.WriteLine("\nAnge önskad diet - Vego/kött/fisk:");
    var diet = Console.ReadLine().ToLower();

    while (diet != "vego" && diet != "fisk" && diet != "kött")
    {
        Console.WriteLine("Vänligen ange vego, fisk eller kött:");
        diet = Console.ReadLine().ToLower();
    }

    Console.WriteLine("\nTillgänliga Lunchlådor:");
    foreach (var lb in activeUser.GetAvailableLunchBoxes(diet))
    {
        Console.WriteLine($"{lb.DishName}, {Decimal.Round(lb.Price)} kronor, {lb.Restaurant.Name}, Köp-ID: {lb.Id}");
    }
    Console.WriteLine("\nTryck esc för att gå tillbaka till menyn, enter för att köpa en lunchlåda:");
    
    var key = Console.ReadKey().Key;
    if (key == ConsoleKey.Escape)
    {
        return;
    }

    var foodChoice=0;
    Console.WriteLine("\nKöp en lunchlåda genom att ange dess Köp-ID:");

    while (true)
    {
        try
        {
            foodChoice = Convert.ToInt32(Console.ReadLine());
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Vänligen ange matlådans köp-id (i heltal) för att köpa lådan.");
        }
    }
    
 
    LunchBox lunchBoxToBuy =
            activeUser.BuyThisLunchBox(foodChoice);
    if (lunchBoxToBuy != null)
    {
        Console.WriteLine($"Tack för ditt köp av {lunchBoxToBuy.DishName}. Lunchlådan kan hämtas hos {lunchBoxToBuy.Restaurant.Name}.");        //TODO Lunchlådan hämtas hos xx restaurang
    }
    else
    {
        Console.WriteLine("Matlådan kan inte köpas, vänligen ange ett nytt Köp-ID.");
    }

    Console.ReadLine();
}
#endregion

#region Hjälpmetod SeePreviousOrders()
void SeePreviousOrders(User user)
{
    UserBackend activeUser = new UserBackend(user);
    Console.Clear();
    Console.WriteLine("Dina tidigare köp:");
    foreach (var i in activeUser.GetBoughtBoxes())
    {
        Console.WriteLine($"\nOrdernummer: {i.Id}, Orderdatum: {i.SalesDate}");
        foreach (var lb in i.LunchBoxes)
        {
            Console.WriteLine($"{lb.DishName}, {lb.Restaurant.Name}, {decimal.Round(lb.Price)} kronor");
        }
    }
    Console.ReadLine();
}
#endregion

#region Hjälpmetod RegisterUser()
User RegisterUser()
{
    Console.Clear();
    Console.WriteLine("Registrera användare");

    Console.WriteLine("\nAnge ditt för- och efternamn:");
    var name = Console.ReadLine();

    Console.WriteLine("Ange ett användarnamn:");
    var userName = Console.ReadLine();

    Console.WriteLine("Ange ett lösenord:");
    var passWord = Console.ReadLine();

    Console.WriteLine("Ange din mejladress");
    var mail = Console.ReadLine();

    UserBackend.CreateUser(name, userName, passWord, mail);

    Console.WriteLine($"Välkommen {name}. Du är nu registrerad och inloggad.");
    

    //Skapar en inloggad användare som skickar tillbaka
    var user = UserBackend.TryLogin(name, passWord);
    return user;

}
#endregion

#region ProgramLoopLoggedInCust()
void ProgramLoopLoggedInCust(User user)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Välkommen " + user.PersonalInfo.FullName + "!");
        Console.WriteLine("Vad vill du göra? Välj 1 eller 2:");
        Console.WriteLine("\n1. Se och köpa tillgängliga lunchlådor");
        Console.WriteLine("2. Se dina tidigare köp"); //TODO Dela upp i undermeny, baserat på ordernummer, eller alla, totala ordersumman 

        var choiceLoggedIn = Console.ReadLine();

        switch (choiceLoggedIn)
        {
            case "1": //Se och köpa lunchlådor
                SeeAndBuyLunchbox(user);
                break;

            case "2": //Se tidigare köp
                SeePreviousOrders(user);
                break;

            default:
                Console.WriteLine("Vänligen gör ditt val genom att ange siffran 1 eller 2:");
                break;
        }
    }
}
#endregion

#region Login()
void Login()
{
    Console.WriteLine("Ange användarnamn:");
    var username = Console.ReadLine();
    Console.WriteLine("Ange lösenord:");
    var password = Console.ReadLine();

    var user = UserBackend.TryLogin(username, password);

    if (user == null)
    {
        Console.WriteLine("Login misslyckades!");
        Console.ReadLine();
    }
    else { ProgramLoopLoggedInCust(user); }
}
#endregion