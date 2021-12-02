﻿using System.Security.Cryptography.X509Certificates;
using DataLayer.Backend;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

{
    //Databasen skapas via AdminTool, en gång av admin
    var optionBuilder = new DbContextOptionsBuilder();
    optionBuilder.UseSqlServer(
        @"server=(localdb)\MSSQLLocalDB;database=FoodRescueLiveDb"); 

    AdminBackend admin = new AdminBackend(optionBuilder.Options);
    var userBackend = new UserBackend(optionBuilder.Options);


    #region Login

    while (true)
    {

        Console.Clear();
        //För login använd exempelvis Pia.Hagman och lösenord HelloWorld1
        Console.WriteLine("Inlogg Admin");
        Console.Write("Användarnamn: ");
        var userName = Console.ReadLine();
        Console.Write("Lösenord: ");
        var password = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                Console.Write("\b \b");
                password = password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                password += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        //var password = Console.ReadLine();
        var user = userBackend.TryAdminLogin(userName, password);

        if (user == null)
        {
            Console.Clear();

            Console.WriteLine("Inloggning misslyckades");
            Console.ReadLine();

        }
        else
        {
            MainLoop(user);
        }

    }

    #endregion

    #region MainLoop
    void MainLoop(User user)
    {
        bool exit = true;
        while (exit)
        {
            #region Meny Admin

            Console.Clear();
            Console.WriteLine("Vad vill du göra? Ange ditt val 1-6:\n");
            Console.WriteLine("1. Se alla kunder");
            Console.WriteLine("2. Radera en kund");
            Console.WriteLine("3. Se alla restauranger");
            Console.WriteLine("4. Lägga till en ny restaurang");
            Console.WriteLine("5. Återställa databasen");
            Console.WriteLine("6. Skapa Adminanvändare");
            Console.WriteLine("7. Se Adminanvändare");
            Console.WriteLine("0. Avsluta");

            #endregion

            var choice = Console.ReadLine();

            switch (choice)
            {
                #region case 1: Se alla kunder

                case "1":
                    Console.Clear();
                    Console.WriteLine("Kunder Food Rescue:\n");
                    foreach (var infoUser in admin.GetUsers())
                    {
                        if (infoUser.PersonalInfo == null)
                        {
                            Console.WriteLine($"Id: {infoUser.Id}, Personlig information saknas.");
                        }
                        else
                            Console.WriteLine
                                ($"Id: {infoUser.Id}, Namn: {infoUser.PersonalInfo.FullName}, Användarnamn: {infoUser.PersonalInfo.Username}, Lösenord: {infoUser.PersonalInfo.Password}, Email: {infoUser.PersonalInfo.Email}");
                    }

                    Console.ReadLine();
                    break;

                #endregion

                #region case 2: Radera en kund

                case "2":

                    Console.Clear();
                    Console.Clear();
                    Console.WriteLine("Kunder Food Rescue:\n");
                    foreach (var infoUser in admin.GetUsers())
                    {
                        Console.WriteLine(
                            $"Id: {infoUser.Id}, Namn: {infoUser.PersonalInfo.FullName}, Användarnamn: {infoUser.PersonalInfo.Username}, Lösenord: {infoUser.PersonalInfo.Password}, Email: {infoUser.PersonalInfo.Email}");
                    }

                    Console.WriteLine("\nAnge vilken användare du vill radera genom att ange användarnamnet:");
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

                #region case 5: Reset database

                case "5":
                    Console.Clear();
                    Console.WriteLine("För att återställa databasen skriv \"ja\"");
                    var resetDatabase = Console.ReadLine();

                    var database = new Database(optionBuilder.Options);

                if (resetDatabase.ToLower() == "ja")
                {
                    database.Recreate();
                    database.SeedTestData();
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

                #region case 6: Create Admin user
                case "6":
                    RegisterAdminUser();
                    break;

                #endregion

                #region See AdminUsers

                case "7":
                    Console.Clear();
                    Console.WriteLine("Adminanvändare\n");
                    foreach (var infoUser in admin.GetUsers())
                    {
                        if (infoUser.PersonalInfo == null)
                        {
                            Console.WriteLine($"Id: {infoUser.Id}, Personlig information saknas.");
                        }

                        if (infoUser.Admin == true)
                        {
                            Console.WriteLine
                                ($"Id: {infoUser.Id}, Namn: {infoUser.PersonalInfo.FullName}, Användarnamn: {infoUser.PersonalInfo.Username}" +
                                 $", Lösenord: {infoUser.PersonalInfo.Password}, Email: {infoUser.PersonalInfo.Email}");
                        }
                         
                    }

                    Console.ReadLine();
                    break;


                #endregion

                #region case 0: Exit program

            case "0":
                {
                    exit = false;
                    break;
                }
            #endregion
        }

        }
    }
    #endregion


    void RegisterAdminUser()
    {
        Console.Clear();
        Console.WriteLine("Registrera användare");

        Console.Write("\nAnge ditt för- och efternamn:");
        var name = Console.ReadLine();

        Console.Write("Ange ett användarnamn:");
        var userName = Console.ReadLine();

        Console.Write("Ange din mejladress");
        var mail = Console.ReadLine();

        Console.Write("Ange ett lösenord:");
        var password = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                Console.Write("\b \b");
                password = password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                password += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);


        userBackend.CreateAdminUser(name, userName, password, mail);

        var user = userBackend.TryLogin(userName, password);

        if (user == null)
        {
            Console.WriteLine("Något gick fel, vänligen försök igen");
            Console.ReadLine();
        }
        else { MainLoop(user); }


    }
}

