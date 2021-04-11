using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User,CarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new CarContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.ID equals userOperationClaim.OperationClaimID
                             where userOperationClaim.UserID == user.ID
                             select new OperationClaim { ID = operationClaim.ID, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public void AddUserOperationClaim(UserOperationClaim userOperationClaim)
        {
            using(var context = new CarContext())
            {
                var addedEntity = context.Entry(userOperationClaim);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public OperationClaim GetClaim(string claim)
        {
            using(var context = new CarContext())
            {
                return context.Set<OperationClaim>().SingleOrDefault(c=>c.Name == claim);
            }
        }
    }
}
