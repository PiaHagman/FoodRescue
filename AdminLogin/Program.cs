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
            Console.WriteLine("Database initialized");
            Thread.Sleep(1000);
            var userBackend = new UserBackend(optionBuilder.Options);
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
                var user = userBackend.TryLogin(userName, password);

                if (user == null)
                {
                    Console.Clear();

                    Console.WriteLine("Inloggning misslyckades");
                    Console.ReadLine();

                }
                else { ProgramLoopLogin(user); }

            }

        }

        #region ProgramLoopLogin
        public static void ProgramLoopLogin(User user)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Hej och välkommen {user.PersonalInfo.FullName}!");
                Console.ReadLine();

            }
        }
        #endregion
    }


}