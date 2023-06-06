using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfBrandRepository : EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
    {
        public EfBrandRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
