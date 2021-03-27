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

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarContext>, ICarDal
    {
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
    }
}
