using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        void GetByID(int ID);
        void GetAll();
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
