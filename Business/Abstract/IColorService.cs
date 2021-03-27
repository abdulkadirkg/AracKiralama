using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService
    {
        void Get(int ID);
        void GetAll();
        void Add(Color car);
        void Update(Color car);
        void Delete(Color car);
    }
}
