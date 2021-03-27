using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<Color> Get(int ID);
        IDataResult<List<Color>> GetAll();
        IResult Add(Color car);
        IResult Update(Color car);
        IResult Delete(Color car);
    }
}
