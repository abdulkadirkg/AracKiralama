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
            //AddCar();
            //AddColor();
            //AddBrand();
            //DeleteBrand();
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.GetDetail(1);
        }

        private static void DeleteBrand()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Delete(new Brand() { ID = 1 });
        }

        private static void AddBrand()
        {
            Brand brand = new Brand();
            brand.BrandName = "Aston Martin";
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(brand);
        }

        private static void AddColor()
        {
            Color color = new Color();
            color.ColorName = "Kahverengi";
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(color);
        }

        private static void AddCar()
        {
            Car car = new Car()
            {
                BrandID = 1,
                ColorID = 1,
                DailyPrice = 125.99M,
                Description = "5.20D",
                ModelYear = 2020,
                BrandName = "BMW"
            };

            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(car);
        }
    }
}
