using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
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
        [SecuredOperation("admin,user")]
        public IDataResult<List<Rental>> GetByCustomer(int customerID)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(c=>c.CustomerID == customerID));
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
