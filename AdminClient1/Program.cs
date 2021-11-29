using System;
using System.Threading;
using DataLayer;
using DataLayer.Backend;

namespace AdminClient1
{
    public class Program
    {
        static void Main(string[] args)
        {
            AdminBackend admin = new AdminBackend();
            admin.CreateAndSeedDb();
            Console.WriteLine("Database initialized");
            Thread.Sleep(2000);

            Console.WriteLine();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Vänligen logga in med Användarnamn/Epost och Lösenord.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Användarnamn/Epost: ");
                var user = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Lösenord: ");
                var password = Console.ReadLine();

            }
        }
    }
}
