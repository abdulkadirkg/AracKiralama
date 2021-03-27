using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            _carDal.Add(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public void Get(int ID)
        {
            _carDal.Get(p=>p.ID == ID);
        }

        public void GetAll()
        {
            _carDal.GetAll();
        }

        public CarDetailDto GetDetail(int ID)
        {
            return _carDal.GetCarDetails(ID);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }
    }
}
