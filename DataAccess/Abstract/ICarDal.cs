using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        CarDetailDto GetCarDetails(int ID);
        CarDetailDto GetWithDetails(int ID);
        List<CarDetailDto> GetAllWithDetails(Expression<Func<IDto, bool>> filter = null);
    }
}
