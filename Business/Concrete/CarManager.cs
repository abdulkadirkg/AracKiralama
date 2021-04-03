using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.AutoFac.Validation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("Product.Add,Admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult();
        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("Product.Delete,Admin")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }
        [CacheAspect]
        [SecuredOperation("Product.Get,Admin")]
        public IDataResult<Car> Get(int ID)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p=>p.ID == ID));
        }
        [CacheAspect]
        [SecuredOperation("Product.GetAll,Admin")]
        [PerformanceAspect(1)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }
        [CacheAspect]
        [SecuredOperation("Product.GetDetail,Admin")]
        public IDataResult<CarDetailDto> GetDetail(int ID)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetails(ID));
        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("Product.Update,Admin")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }
    }
}
