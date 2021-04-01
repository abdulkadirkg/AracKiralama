using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        //IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<User> Get(int ID);
        IDataResult<List<User>> GetAll();
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
    }
}
