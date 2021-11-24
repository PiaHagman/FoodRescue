
//Skapa och seeda databasen

using DataLayer.Backend;

AdminBackend admin = new AdminBackend();
admin.CreateAndSeedDb();
Console.WriteLine("Database initialized");
Thread.Sleep(2000);

Console.WriteLine();
