using Business.Abstract;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<User> Get(int ID)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.ID == ID));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
        [TransactionScopeAspect]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            UserOperationClaim claim = new UserOperationClaim() {
                UserID = user.ID,
                OperationClaimID = _userDal.GetClaim("User").ID
            };
            _userDal.AddUserOperationClaim(claim);
            return new SuccessResult("Kayıt Başarılı");
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.EMail == email);
        }
    }
}
