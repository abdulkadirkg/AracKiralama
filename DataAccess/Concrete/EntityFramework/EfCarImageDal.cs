using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Concrete
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage,CarContext>, ICarImageDal
    {
    }
}
