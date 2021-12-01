using DataLayer.Backend;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace RestaurantClient
{
    public class FrontEndHandler
    {
        private DbContextOptions options;
        private RestaurantBackend restaurantBackend;
        
        public FrontEndHandler()
        {
            var optionBuilder = new DbContextOptionsBuilder();
            optionBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=FoodRescueLiveDb");

            var database = new Database(optionBuilder.Options);
            database.Recreate();
            //database.SeedLiveData(); Används i verkligheten
            database.SeedTestData(); //Används inte i skarpt läge

            restaurantBackend = new RestaurantBackend(optionBuilder.Options);

        }
        // Meny

        public void Menu(){

            Console.WriteLine("\tChoose your options: \n");
            Console.WriteLine("1# Get your sold Foodboxes");
            Console.WriteLine("2# Get your unsold Foodboxes");
            Console.WriteLine("3# Add a foodbox");

        }


        // Osålda matlådor
        public void GetUnsoldFoodboxesSpecifikRestaurant() {

            Console.WriteLine("Choose the restaurantID in order to see which foodboxes are unsold: ");
            var choosenRestaruant = Int32.Parse(Console.ReadLine());

                var loggedInRestaurant = restaurantBackend.FindObjectById(choosenRestaruant);

            List<LunchBox> soldLunchBoxes = restaurantBackend.GetUnsoldLB(loggedInRestaurant);

            Console.WriteLine("\nUnsold or available foodboxes: \n");
                foreach (var e in soldLunchBoxes)
                {
                    Console.WriteLine($"Foodname: {e.DishName}\t Foodtype: {e.DishType}\t Price: {e.Price}");
                }

                Console.WriteLine("");
        }

        // Sålda matlådor
        public void GetSoldFoodboxesForSpecifikRestaurant()
        {
           
            Console.WriteLine("Choose the restaurantID in order to see which foodboxes are sold: ");
            var choosenRestaruant = Int32.Parse(Console.ReadLine());

            var loggedInRestaurant = restaurantBackend.FindObjectById(choosenRestaruant);

            List<LunchBox> soldLunchBoxes = restaurantBackend.GetSoldLB(loggedInRestaurant);

            Console.WriteLine("\nUnsold or available foodboxes: \n");
            foreach (var e in soldLunchBoxes)
            {
                Console.WriteLine($"Foodname: {e.DishName}\t Foodtype: {e.DishType}\t Price: {e.Price}");
            }
            Console.WriteLine("");

        }

        // Köpa matlåda

        public void AddNewFoodbox()
        {
            Console.WriteLine("Choose your RestaurantID: ");
            var choosenRestaruant = Int32.Parse(Console.ReadLine());
            var loggedInRestaurant = restaurantBackend.FindObjectById(choosenRestaruant);

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

            restaurantBackend.AddLunchBox(dishname, dishtype, price, loggedInRestaurant);
            Console.WriteLine("");
            Console.WriteLine("Lunchlådan är tillagd!");
            Console.WriteLine("");

        }

    }

}
