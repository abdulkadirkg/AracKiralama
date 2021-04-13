using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Entities.DTOs;
using AutoMapper;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }
        //[SecuredOperation("admin")]
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(CheckIfNotAvailable(rental));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult("Kiralama Başarılı");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<Rental> Get(int ID)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.ID == ID));
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }
        [SecuredOperation("Admin,User")]
        public IDataResult<List<RentalDetailDto>> GetByCustomer(int ID)
        {
            var getAll = _rentalDal.GetAll(c => c.CustomerID == ID);
            List<RentalDetailDto> rentalDetailDto = new List<RentalDetailDto>();
            foreach (var g in getAll)
            {
                CarDetailDto car = _carService.GetDetail(g.CarID).Data;
                rentalDetailDto.Add(new RentalDetailDto()
                {
                    BrandID = car.BrandID,
                    BrandName = car.BrandName,
                    CarImages = car.CarImages,
                    CarName = car.CarName,
                    CarID = car.ID,
                    ColorID = car.ColorID,
                    ModelYear = car.ModelYear,
                    CustomerID = g.CustomerID,
                    ColorName = car.ColorName,
                    RentDate = g.RentDate,
                    ReturnDate = g.ReturnDate,
                    DailyPrice = car.DailyPrice,
                    Description = car.Description
                }); 
            }
            return new SuccessDataResult<List<RentalDetailDto>>(rentalDetailDto);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        private IResult CheckIfNotAvailable(Rental rental)
        {
            var data = _rentalDal.GetAll(c => c.CarID == rental.CarID);
            if (data.Where(d => d.RentDate >= rental.RentDate).Any() &&
                data.Where(d => d.RentDate <= rental.ReturnDate).Any() &&
                data.Where(d => d.ReturnDate <= rental.ReturnDate).Any() &&
                data.Where(d => d.ReturnDate >= rental.RentDate).Any()
                )
            {
                return new ErrorResult("Bu araç seçilen tarih aralığında müsait değil.");

            }
            return new SuccessResult();
        }
    }
}
