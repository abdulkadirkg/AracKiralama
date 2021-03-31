using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Add(file);
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<CarImage> Get(int ID)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.ID == ID));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarID));
            if (result != null)
            {
                return result;
            }

            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _carImageDal.Get(p => p.ID == carImage.ID).ImagePath;

            carImage.ImagePath = FileHelper.Update(oldPath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }
        public IDataResult<List<CarImage>> GetImagesByCarId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(id).Data);
        }

        private IDataResult<List<CarImage>> CheckIfCarImageNull(int id)
        {
            try
            {
                string path = @"\uploads\default.jpg";
                var result = _carImageDal.GetAll(c => c.CarID == id).Any();
                if (!result)
                {
                    List<CarImage> carImage = new List<CarImage>();
                    carImage.Add(new CarImage { CarID = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<CarImage>>(carImage);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarID == id));
        }

        private IResult CheckImageLimitExceeded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(p => p.CarID == carId).Count;
            if (carImageCount >= 5)
            {
                return new ErrorResult("Max 5 resim eklenebilir");
            }

            return new SuccessResult();
        }
    }
}
