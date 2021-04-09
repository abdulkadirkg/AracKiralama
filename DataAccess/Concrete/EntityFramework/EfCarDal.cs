using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
using Core.Entities.Abstract;
using System.Diagnostics;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarContext>, ICarDal
    {
        public List<CarDetailDto> GetAllWithDetails(Expression<Func<IDto, bool>> filter = null)
        {
            List<CarDetailDto> detailedCar = new List<CarDetailDto>();
            var cars = GetAll();
            using (CarContext context = new CarContext())
            {
                foreach (var car in cars)
                {
                    var result = from i in context.CarImages
                                 join b in context.Brands
                                 on car.BrandID equals b.ID
                                 join c in context.Colors
                                 on car.ColorID equals c.ID
                                 where car.ID == i.CarID
                                 select new CarImage
                                 {
                                     CarID = car.ID,
                                     Date = i.Date,
                                     ID = i.ID,
                                     ImagePath = i.ImagePath
                                 };
                    detailedCar.Add(new CarDetailDto()
                    {
                        BrandName = car.BrandName,
                        ColorName = car.ColorName,
                        CarName = car.CarName,
                        BrandID = car.BrandID,
                        ColorID = car.ColorID,
                        DailyPrice = car.DailyPrice,
                        CarImages = result.ToList(),
                        ID = car.ID,
                        Description = car.Description,
                        ModelYear = car.ModelYear,
                    });
                }
                return detailedCar;
            }
        }

        public CarDetailDto GetWithDetails(int ID)
        {
            CarDetailDto detailedCar;
            var car = GetCarDetails(ID);
            using (CarContext context = new CarContext())
            {
                var result = from i in context.CarImages
                             join b in context.Brands
                             on car.BrandID equals b.ID
                             join c in context.Colors
                             on car.ColorID equals c.ID
                             where car.ID == i.CarID
                             select new CarImage
                             {
                                 CarID = car.ID,
                                 Date = i.Date,
                                 ID = i.ID,
                                 ImagePath = i.ImagePath
                             };
                detailedCar = new CarDetailDto()
                {
                    BrandName = car.BrandName,
                    ColorName = car.ColorName,
                    CarName = car.CarName,
                    BrandID = car.BrandID,
                    ColorID = car.ColorID,
                    DailyPrice = car.DailyPrice,
                    CarImages = result.ToList(),
                    ID = car.ID,
                    Description = car.Description,
                    ModelYear = car.ModelYear
                };

                return detailedCar;
            }
        }

        private List<CarDetailDto> GetAll()
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandID equals b.ID
                             join clr in context.Colors
                             on c.ColorID equals clr.ID
                             select new CarDetailDto
                             {
                                 ID = c.ID,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 BrandID = c.BrandID,
                                 ColorID = c.ColorID,
                                 ColorName = clr.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetails(int ID)
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Cars
                             where c.ID == ID
                             join b in context.Brands
                             on c.BrandID equals b.ID into gj
                             from subpet in gj.DefaultIfEmpty()
                             join cl in context.Colors
                             on c.ColorID equals cl.ID into gj2
                             from subpet2 in gj2.DefaultIfEmpty()
                             where c.ID == ID
                             select new CarDetailDto
                             {
                                 CarName = c.CarName,
                                 BrandID = c.BrandID,
                                 BrandName = subpet.BrandName,
                                 ColorID = c.ColorID,
                                 ColorName = subpet2.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 ID = c.ID,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear
                             };
                return result.FirstOrDefault();
            }
        }

        public List<CarDetailDto> DetailManager(List<CarDetailDto> list)
        {
            List<CarDetailDto> detailedCar = new List<CarDetailDto>();
            using (CarContext context = new CarContext())
            {
                foreach (var car in list)
                {
                    var result = from i in context.CarImages
                                 join b in context.Brands
                                 on car.BrandID equals b.ID
                                 join c in context.Colors
                                 on car.ColorID equals c.ID
                                 where car.ID == i.CarID
                                 select new CarImage
                                 {
                                     CarID = car.ID,
                                     Date = i.Date,
                                     ID = i.ID,
                                     ImagePath = i.ImagePath
                                 };
                    detailedCar.Add(new CarDetailDto()
                    {
                        BrandName = car.BrandName,
                        ColorName = car.ColorName,
                        CarName = car.CarName,
                        BrandID = car.BrandID,
                        ColorID = car.ColorID,
                        DailyPrice = car.DailyPrice,
                        CarImages = result.ToList(),
                        ID = car.ID,
                        Description = car.Description,
                        ModelYear = car.ModelYear,
                    });
                }
                return detailedCar;
            }
        }
    }
}
