using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Backend
{
    internal class Program //vad fyller denna för funktion?
    {
        internal static void Main()
        {
            var optionBuilder = new DbContextOptionsBuilder();
            optionBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=FoodRescueLiveDb");

            var database = new Database(optionBuilder.Options);

            database.Recreate();
            database.SeedLiveData();
        }
    }
}

