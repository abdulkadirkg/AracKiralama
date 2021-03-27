using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public void Add(Car car)
        {
            using (CarContext context = new CarContext())
            {
                var addedEntity = context.Entry(car);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Car car)
        {
            using (CarContext context = new CarContext())
            {
                var deletedEntity = context.Entry(car);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<Car> GetAll()
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Cars
                             select c;
                return result.ToList();
            }
        }

        public Car GetByID(int ID)
        {
            using (CarContext context = new CarContext())
            {
                var result = from c in context.Cars
                             where c.ID == ID
                             select c;
                return (Car)result;
            }
        }

        public void Update(Car car)
        {
            using (CarContext context = new CarContext())
            {
                var updatedEntity = context.Entry(car);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
