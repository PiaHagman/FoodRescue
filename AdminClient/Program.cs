using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading;
using DataLayer;
using DataLayer.Backend;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminClient
{
    public class Program
    {
        internal static void Main(string[] args)
        {
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

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Inlogg Admin");
                Console.WriteLine();
                Console.Write("Användarnamn: ");
                var userName = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Lösenord: ");
                var password = Console.ReadLine();
                var user = userBackend.TryLogin(userName, password);

                try
                {
                   

                }
                catch (Exception)
                {

                }


            }

        }

        public static void ProgramLoopLogin(UserPersonalInfo user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Hej och välkommen {user.FullName}!");
            }
        }
    }


}