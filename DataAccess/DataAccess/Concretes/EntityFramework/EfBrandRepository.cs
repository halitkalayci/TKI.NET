using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfBrandRepository : IBrandRepository
    {
        public void Add(Brand entity)
        {
            using(BaseDbContext context = new BaseDbContext())
            {
                // context.Add(entity);
                context.Brands.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                Brand brandToDelete = GetById(id);
                context.Remove(brandToDelete);
                context.SaveChanges();
            }
        }

        public List<Brand> GetAll()
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                return context.Brands.ToList();
            }
        }

        public Brand GetById(int id)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                return context.Brands.Where(i => i.Id == id).FirstOrDefault();
            }
        }

        public Brand GetByName(string name)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                return context.Brands.FirstOrDefault(i => i.Name == name);
            }
        }

        public void Update(Brand entity)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
