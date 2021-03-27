using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public void Add(Color color)
        {
            _colorDal.Add(color);
        }

        public void Delete(Color color)
        {
            _colorDal.Delete(color);
        }

        public void Get(int ID)
        {
            _colorDal.Get(p => p.ID == ID);
        }

        public void GetAll()
        {
            _colorDal.GetAll();
        }

        public void Update(Color color)
        {
            _colorDal.Update(color);
        }
    }
}
