﻿using DataLayer.Backend;
using Microsoft.EntityFrameworkCore;

// TODO Flytta ut Se alla kunder till egen metod

{
    //Skapa och seeda databasen
    var optionBuilder = new DbContextOptionsBuilder();
    optionBuilder.UseSqlServer(
        @"server=(localdb)\MSSQLLocalDB;database=FoodRescueLiveDb");

    var database = new Database(optionBuilder.Options);
    database.Recreate();
    //database.SeedLiveData(); Används i verkligheten
    database.SeedTestData(); //Används inte i skarpt läge

    Console.WriteLine("Database initialized");
    Thread.Sleep(1000);
    var userBackend = new UserBackend(optionBuilder.Options);
    Console.WriteLine("Database initialized\n");
    //Thread.Sleep(2000);

    bool exit = true; // Skapar en Exit för programmet
    while (exit)
    {
        #region Meny Admin

        Console.Clear();
        Console.WriteLine("Vad vill du göra? Ange ditt val 1-5:\n");
        Console.WriteLine("1. Se alla kunder");
        Console.WriteLine("2. Radera en kund");
        Console.WriteLine("3. Se alla restauranger");
        Console.WriteLine("4. Lägga till en ny restaurang");
        Console.WriteLine("5. Återställa databasen");
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
                // TODO Blir en bugg när Se alla kunder ska köras efter radering av kund

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

                if (resetDatabase.ToLower() == "ja")
                {
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


            #region case 0: Reset database

            case "0":
                {
                    exit = false;
                    break;

                }

                #endregion


        }

    }
}
