using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using System.Threading;
using DataLayer.Backend;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ConsoleApp
{
    class Program
    {
        //TODO Bryt ut consoleprogrammet i hjälpmetoder REFACTORING!!
        //TODO kolla på ESC-möjlighet för fler metoder
        //TODO Om kund inte är kopplat till köp så ska HELA kunden raderas, ink id-nummer.
        //TODO Gör om registrera kund så att ett userobjekt skickas tillbaka.
        //TODO hoppar ut till "fel" meny i admin och kund

        static void Main(string[] args)
        {
            //Skapa och seeda databasen
            AdminBackend admin = new AdminBackend();
            admin.CreateAndSeedDb();
            Console.WriteLine("Database initialized");
            Thread.Sleep(2000);

            Console.WriteLine();

            while (true)

                #region Huvudmeny

            {
                Console.Clear();
                Console.WriteLine("Välj användartyp genom att ange 1-3");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Kund");
                Console.WriteLine("3. Restaurang");

                var typeOfUser = Console.ReadLine();

                switch (typeOfUser)

                    #endregion

                {
                    #region Admin

                    case "1":
                        //while (true)
                        //{
                            #region Meny Admin

                            Console.Clear();
                            Console.WriteLine("Vad vill du göra? Ange ditt val 1-6:");
                            Console.WriteLine();
                            Console.WriteLine("1. Se alla kunder");
                            Console.WriteLine("2. Radera en kund"); //TODO lägga till att uppdatera/radera personlig info
                            Console.WriteLine("3. Se alla restauranger");
                            Console.WriteLine("4. Lägga till en ny restaurang");
                            Console.WriteLine("5. Radera osålda lunchlådor med utgånget datum");
                            Console.WriteLine("6. Återställa databasen");

                            #endregion

                            var choice = Console.ReadLine();

                            switch (choice)
                            {
                                #region case 1: Se alla kunder

                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Kunder Food Rescue:");
                                    Console.WriteLine();
                                    foreach (var infoUser in admin.GetUsers())
                                    {
                                        if (infoUser.PersonalInfo == null)
                                        {
                                            Console.WriteLine($"Id: {infoUser.Id}, Personlig information saknas.");
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                $"Id: {infoUser.Id}, Namn: {infoUser.PersonalInfo.FullName}, Användarnamn: {infoUser.PersonalInfo.Username}, Lösenord: {infoUser.PersonalInfo.Password}, Email: {infoUser.PersonalInfo.Email}");
                                        }
                                    }
                                    Console.ReadLine();
                                    break;

                                #endregion

                                #region case 2: Radera en kund

                                case "2": 

                                    Console.Clear();
                                    Console.WriteLine("Ange vilken användare du vill radera genom att ange användarnamnet:");
                                    string deleteName = Console.ReadLine();

                                    bool couldDeleteUser = admin.DeleteUser(deleteName);

                                    if (couldDeleteUser)
                                    {
                                        Console.WriteLine($"Den personliga informationen för {deleteName} har raderats.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Användarnamnet du angivit kan inte hittas.");
                                    }
                                    Console.ReadLine();
                                    break;

                                #endregion

                                #region case 3: Se alla restauranger

                                case "3": 
                                    Console.Clear();
                                    Console.WriteLine("Restauranger Food Rescue:");
                                    Console.WriteLine();
                                    foreach (var r in admin.GetRestaurants())
                                    {
                                        Console.WriteLine($"{r.Id}, {r.Name}, {r.Address}, {r.PhoneNumber}");
                                    }
                                    Console.ReadLine();
                                    break;

                                #endregion

                                #region case 4: Lägga till restaurang

                                case "4": 
                                    Console.Clear();
                                    Console.WriteLine("Ny restaurang");

                                    Console.WriteLine("\nAnge namnet på restaurangen:");
                                    string name = Console.ReadLine();
                                    Console.WriteLine("Ange adress:");
                                    string address = Console.ReadLine();
                                    Console.WriteLine("Ange telefonnummer:");
                                    string number = Console.ReadLine();

                                    admin.AddRestaurant(name, address, number);

                                    Console.WriteLine("\nDu har nu lagt till " + name + ".");

                                    Console.ReadLine();
                                    break;

                                #endregion

                                #region case 5: Radera lunchlådor med kort datum

                                case "5": 
                                    Console.Clear();
                                    Console.WriteLine("Radera osålda unchlådor med utgånget datum");

                                    Console.WriteLine("Följande osålda lunchlådor har passerat bäst-före-datum:");
                                    Console.WriteLine();
                                    foreach (var lb in admin.GetExpiredLunchBoxes())
                                    {
                                        Console.WriteLine(
                                            $"{lb.DishName}, {lb.DishType}, {Decimal.Round(lb.Price)} kronor");
                                    }

                                    Console.WriteLine("\nVill du radera lunchlådorna, ange ja/nej:");
                                    var delete = Console.ReadLine();
                                    if (delete.ToLower() == "ja")
                                    {
                                        admin.DeleteLunchBoxes();
                                        Console.WriteLine("\nLunchlådorna har raderats!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nLunchlådorna har inte raderats.");
                                    }

                                    Console.ReadLine();
                                    break;

                                #endregion

                                #region Reset database
                                case "6": 
                                    Console.Clear();
                                    Console.WriteLine("För att återställa databasen skriv \"ja\"");
                                    var resetDatabase = Console.ReadLine();

                                    if (resetDatabase.ToLower() == "ja")
                                    {
                                        admin.CreateAndSeedDb();
                                        Console.WriteLine("Databasen har återställts.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nDatabasen har inte återställts.");
                                    }

                                    Console.ReadLine();
                                    break;

                                default:
                                    Console.WriteLine("Vänligen ange en siffra mellan 1-5:");
                                    break;
                                #endregion
                            }
                            //break; //Denna vill jag inte ha här - gör så att jag kommer tillbaka till huvudmenyn...
                        //} 
                        
                        break;
                

                #endregion

                    #region Kund/User

                    case "2":

                        while (true)
                        {
                            #region MenyUser

                            Console.Clear();
                            Console.WriteLine("Vad vill du göra? Välj 1 eller 2:");
                            Console.WriteLine();
                            Console.WriteLine("1. Logga in. För testkund använd - användarnamn: pia.hagman, lösenord: HelloWorld1");
                            Console.WriteLine("2. Registrera ny användare");   

                            var userChoice = Console.ReadLine();

                            #endregion

                            switch (userChoice)
                            {
                                #region Case 1: Logga in

                                case "1":
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
                                    else

                                        #endregion

                                        #region Inloggad Användare

                                    {
                                        UserBackend activeUser = new UserBackend(user); //Skapar ett objekt av userManager och skickar med snvändaren så att den kan göra val som inloggad.

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
                                                #region case 1: Se och köpa lunchlådor

                                                case "1":
                                                    Console.Clear();
                                                    Console.WriteLine("Lunchlådor Food Rescue:");
                                                    Console.WriteLine("\nAnge önskad diet - Vego/kött/fisk:");
                                                    var diet = Console.ReadLine().ToLower();

                                                    if (diet != "vego" && diet != "fisk" && diet != "kött")
                                                    {
                                                        Console.WriteLine("Vänligen ange vego, fisk eller kött:");
                                                        diet = Console.ReadLine().ToLower();
                                                    }

                                                    Console.WriteLine("\nTillgänliga Lunchlådor:");
                                                    foreach (var lb in activeUser.GetAvailableLunchBoxes(diet))
                                                    {
                                                        Console.WriteLine($"{lb.DishName}, {Decimal.Round(lb.Price)} kronor, {lb.Restaurant.Name}, Köp-ID: {lb.Id}");
                                                    }
                                                    Console.WriteLine("\nTryck esc för att logga ut, enter för att köpa en lunchlåda:");
                                                    var key = Console.ReadKey().Key;
                                                    if (key == ConsoleKey.Escape)
                                                    {
                                                        break;
                                                    }
                                                    Console.WriteLine("\nKöp en lunchlåda genom att ange dess Köp-ID:");
                                                    var foodChoice = Console.ReadLine();

                                                    bool canBuyThisLunchBox =
                                                            activeUser.BuyThisLunchBox(Convert.ToInt32(foodChoice));
                                                    if (canBuyThisLunchBox)
                                                    {
                                                        Console.WriteLine("Tack för ditt köp!");        //TODO Lunchlådan hämtas hos xx restaurang
                                                        Console.ReadLine();
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Matlådan kan inte köpas, vänligen ange ett nytt Köp-ID.");
                                                    }

                                                    Console.ReadLine();
                                                    break;

                                                #endregion

                                                #region Case 2: Se tidigare köp

                                                case "2":
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
                                                    break;

                                                #endregion

                                                default:
                                                    Console.WriteLine("Vänligen ange siffran 1 eller 2:");
                                                    break;
                                            }

                                            break;
                                        }
                                    }

                                    break;

                                #endregion

                                #region case 2: Registrera ny användare 

                                case "2":
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

                                    Console.WriteLine($"Välkommen {name}. Du är nu registrerad!");
                                    Console.ReadLine();
                                    break;

                                #endregion
                                
                                default:
                                    Console.WriteLine("Vänligen Ange 1 eller 2:");
                                    break;

                            }
                            break;
                        }
                        break;
                    #endregion

                    #region Restaurang
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Vänligen ange ditt id:");
                        var restaurantId = Convert.ToInt32(Console.ReadLine());

                        //Får tillbaka ett restuarangobjekt om ID hittas
                        RestaurantBackend restaurant = new RestaurantBackend();
                        var loggedInRestaurant = new RestaurantBackend().FindObjectById(restaurantId);

            

                        if (loggedInRestaurant != null)
                        {
                            while (true)
                            {
                                #region MenyRestaurant
                                Console.Clear();
                                Console.WriteLine("Välkommen " + loggedInRestaurant.Name + "!");
                                Console.WriteLine("\nVad vill du göra? Välj 1-3:");
                                Console.WriteLine();
                                Console.WriteLine("1. Se alla sålda lunchlådor");
                                Console.WriteLine("2. Se försäljning per månad");
                                Console.WriteLine("3. Lägga till ny lunchlåda");

                                var restaurantChoice = Console.ReadLine();

                                #endregion

                                switch (restaurantChoice)
                                {
                                    #region Case 1: Se alla sålda matlådor //TODO Lägg till ESC möjlighet

                                    case "1":

                                        List<LunchBox> soldLunchBoxes =
                                        restaurant.GetSoldLB(loggedInRestaurant);
                                        Console.Clear();
                                        Console.WriteLine("Dina sålda matlådor:");

                                        foreach (var lb in soldLunchBoxes)
                                        {
                                            Console.WriteLine(
                                                   $"Maträtt: {lb.DishName}, Typ: {lb.DishType}, Pris: {Decimal.Round(lb.Price)} kronor");
                                        }
                                        Console.ReadLine();
                                        break;

                                    #endregion

                                    #region Inkomst/månad
                                    case "2":
                                        Console.Clear();
                                        Console.WriteLine("Ange vilken månad du vill få försäljningsstatistik för, genom att ange 1 för januari," +
                                                          " 2 för februari osv:");
                                        int month = Convert.ToInt32(Console.ReadLine());

                                        decimal income = restaurant.GetIncomePerMonth(month, loggedInRestaurant);
                                        if (income <= 0)
                                        {
                                            Console.WriteLine("Du har ingen inkomst denna månad.");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Summa: {Decimal.Round(income)} kronor.");
                                        }
                                        
                                        //TODO Console.WriteLine("\n Vill du se inkomst för en annan månad?");

                                        Console.ReadLine();
                                        break;
                                    #endregion

                                    #region Case 3: Lägg till en ny lunchlåda
                                    case "3":
                                        Console.Clear();
                                        Console.WriteLine("Lägg till en ny lunchlåda:");
                                        Console.WriteLine("\nAnge maträttens namn:");
                                        var dishname = Console.ReadLine();
                                        Console.WriteLine("Ange mattyp: vego/fisk/kött:");
                                        var dishtype = Console.ReadLine().ToLower();

                                        if (dishtype != "vego" && dishtype != "fisk" && dishtype != "kött")
                                        {
                                            Console.WriteLine("Vänligen ange vego, fisk eller kött:");
                                            dishtype = Console.ReadLine().ToLower();
                                        }

                                        decimal price;
                                        while (true)
                                        {
                                            Console.WriteLine("Ange maträttens pris:");
                                            try
                                            {
                                                price = Convert.ToDecimal(Console.ReadLine());
                                                break;
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Vänligen använd siffror för att ange pris.");
                                            }
                                        }
                                        
                                        
                                        
                                        restaurant.AddLunchBox(dishname, dishtype, price, loggedInRestaurant);
                                        Console.WriteLine("Lunchlådan är tillagd!");
                                        Console.ReadLine();
                                        break;
                                    #endregion

                                    default:
                                        Console.WriteLine("Vänligen välj 1 eller 2:");
                                        Console.ReadLine();
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ditt id stämmer inte, vänligen försök igen eller tryck ESC för att avsluta:");
                            Console.ReadLine();
                        }
                        break;

                     default:
                        Console.WriteLine("Ogiltig inmatning. Försök igen!");
                        Console.ReadLine();
                        break;
                        #endregion
                }
            }
        }
    }

}