using DataLayer.Backend;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class FrontEndHandler
    {

        // Meny

        public void Menu(){

            Console.WriteLine("Choose your options: \n");
            Console.WriteLine("Get your sold Foodboxes");
            Console.WriteLine("Get your unsold Foodboxes");
            Console.WriteLine("Add a foodbox");

        }


        // Osålda matlådor
        public void GetUnsoldFoodboxesSpecifikRestaurant() {

            RestaurantBackend restaurantBack = new RestaurantBackend();
            Console.WriteLine("Choose the restaurantID in order to see which foodboxes are unsold: ");
            var choosenRestaruant = Int32.Parse(Console.ReadLine());

                var loggedInRestaurant = new RestaurantBackend().FindObjectById(choosenRestaruant);

            List<LunchBox> soldLunchBoxes = restaurantBack.GetUnsoldLB(loggedInRestaurant);

            Console.WriteLine("\nUnsold or available foodboxes: \n");
                foreach (var e in soldLunchBoxes)
                {
                    Console.WriteLine($"Foodname: {e.DishName}\t Foodtype: {e.DishType}\t Price: {e.Price}");
                }
        }

        // Sålda matlådor
        public void GetSoldFoodboxesForSpecifikRestaurant()
        {
            RestaurantBackend restaurantBack = new RestaurantBackend();
            Console.WriteLine("Choose the restaurantID in order to see which foodboxes are sold: ");
            var choosenRestaruant = Int32.Parse(Console.ReadLine());

            var loggedInRestaurant = new RestaurantBackend().FindObjectById(choosenRestaruant);

            List<LunchBox> soldLunchBoxes = restaurantBack.GetSoldLB(loggedInRestaurant);

            Console.WriteLine("\nUnsold or available foodboxes: \n");
            foreach (var e in soldLunchBoxes)
            {
                Console.WriteLine($"Foodname: {e.DishName}\t Foodtype: {e.DishType}\t Price: {e.Price}");
            }
        }

        // Köpa matlåda

        public void AddNewFoodbox()
        {
            RestaurantBackend restaurantBack = new RestaurantBackend();
            Console.WriteLine("Choose your RestaurantID: ");
            var choosenRestaruant = Int32.Parse(Console.ReadLine());
            var loggedInRestaurant = new RestaurantBackend().FindObjectById(choosenRestaruant);

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

            restaurantBack.AddLunchBox(dishname, dishtype, price, loggedInRestaurant);
            Console.WriteLine("Lunchlådan är tillagd!");
        }

    }

}
