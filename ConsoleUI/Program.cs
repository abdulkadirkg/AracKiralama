using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car()
            {
                BrandID = 1,
                ColorID = 1,
                DailyPrice = 125.00,
                Description = "5.20D",
                ModelYear = 2020,
                BrandName = "BMW"
            };

            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(car);
        }
    }
}
