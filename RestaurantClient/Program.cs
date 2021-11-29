// See https://aka.ms/new-console-template for more information
using DataLayer.Backend;
using DataLayer.Model;
using RestaurantClient;

FrontEndHandler frontEndHandler = new FrontEndHandler();

while (true)
{
    frontEndHandler.Menu();
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":

            frontEndHandler.GetSoldFoodboxesForSpecifikRestaurant();

            break;

        case "2":

            frontEndHandler.GetUnsoldFoodboxesSpecifikRestaurant();

            break;
               
        case "3":

            frontEndHandler.AddNewFoodbox();
            frontEndHandler.GetUnsoldFoodboxesSpecifikRestaurant();

            break;

        case "X":
            Environment.Exit(1);
            break;

        default:
            Console.WriteLine("If you want to exit: Press \"X\"\n");
            break;

    }


}





