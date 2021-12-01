using DataLayer.Backend;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

var optionBuilder = new DbContextOptionsBuilder();
optionBuilder.UseSqlServer(
    @"server=(localdb)\MSSQLLocalDB;database=FoodRescueLiveDb");

var userBackend = new UserBackend(optionBuilder.Options);

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
            RegisterUser();
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
    Console.Clear();
    Console.WriteLine("Lunchlådor Food Rescue:");
    Console.WriteLine("\nAnge önskad diet - Vego/kött/fisk:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    var diet = Console.ReadLine().ToLower();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    while (diet != "vego" && diet != "fisk" && diet != "kött")
    {
        Console.WriteLine("Vänligen ange vego, fisk eller kött:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        diet = Console.ReadLine().ToLower();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    Console.WriteLine("\nTillgänliga Lunchlådor:");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    if (userBackend.GetAvailableLunchBoxes(diet).Count == 0)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    {
        Console.WriteLine($"Det finns tyvärr inga tillgängliga lunchlådor i kategorin {diet}.");
        Console.ReadLine();
        return;
    }
    foreach (var lb in userBackend.GetAvailableLunchBoxes(diet))
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
            userBackend.BuyThisLunchBox(foodChoice, user);
    if (lunchBoxToBuy != null)
    {
        Console.WriteLine($"Tack för ditt köp av {lunchBoxToBuy.DishName}. Lunchlådan kan hämtas hos {lunchBoxToBuy.Restaurant.Name}.");
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
    Console.Clear();
    Console.WriteLine("Dina tidigare köp:");

    if (userBackend.GetBoughtBoxes(user).Count == 0)
    {
        Console.WriteLine("Du har inte gjort några tidigare köp.");
    }
    foreach (var i in userBackend.GetBoughtBoxes(user))
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
void RegisterUser()
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

    
    userBackend.CreateUser(name, userName, passWord, mail);

    var user = userBackend.TryLogin(userName, passWord);

    if (user == null)
    {
        Console.WriteLine("Något gick fel, vänligen försök igen");
        Console.ReadLine();
    }
    else { ProgramLoopLoggedInCust(user); }


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
        Console.WriteLine("2. Se dina tidigare köp");

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

    var user = userBackend.TryLogin(username, password);

    if (user == null)
    {
        Console.WriteLine("Login misslyckades!");
        Console.ReadLine();
    }
    else { ProgramLoopLoggedInCust(user); }
}
#endregion