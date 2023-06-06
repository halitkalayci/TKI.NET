using Core.DataAccess.EntityFramework;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfUserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
    {
        public EfUserRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
