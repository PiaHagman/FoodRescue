using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading;
using DataLayer;
using DataLayer.Backend;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminClient1
{
    public class Program
    {
        internal static void Main(string[] args)
        {
            //AdminBackend admin = new AdminBackend();
            //admin.CreateAndSeedDb();
            //Console.WriteLine("Database initialized");
            //Thread.Sleep(2000);

            //Console.WriteLine();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Inlogg Admin");
                Console.WriteLine();
                Console.Write("Användarnamn/Epost: ");
                var userName = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Lösenord: ");
                var password = Console.ReadLine();

                //var user = UserBackend.TryLogin(userName, password);

                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseSqlServer(
                        @"server=(localdb)\MSSQLLocalDB;database=FoodRescueDb_CodeFirst_Live");
                    var login = new Login(optionsBuilder.Options);
                    var user = login.User(userName, password);
                    ProgramLoopLogin(user);

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
