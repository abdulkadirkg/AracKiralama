using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<Car> Get(int ID);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetByBrandId(int ID);
        IDataResult<List<Car>> GetByColorId(int ID);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<CarDetailDto> GetDetail(int ID);
        IDataResult<List<CarDetailDto>> GetAllWithDetails();
    }
}
