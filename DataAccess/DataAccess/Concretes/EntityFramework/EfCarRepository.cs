using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfCarRepository : ICarRepository
    {
        public void Add(Car car)
        {
            // using scope
            using (BaseDbContext context = new BaseDbContext())
            {
                context.Cars.Add(car);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using(BaseDbContext context = new BaseDbContext())
            {
                Car car = GetById(id);
                context.Cars.Remove(car);
                context.SaveChanges();
            }
        }

        public List<Car> GetAll()
        {
            using(BaseDbContext context = new BaseDbContext())
            {
                return context.Cars.Include(i=>i.Color).ToList();
            }
        }

        public Car GetById(int id)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                return context.Cars.FirstOrDefault(i=>i.Id == id);
            }
        }

        public Car GetByPlate(string plate)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                return context.Cars.FirstOrDefault(i => i.Plate == plate);
            }
        }

        public void Update(Car car)
        {
            using (BaseDbContext context = new BaseDbContext())
            {
                context.Cars.Update(car);
                context.SaveChanges();
            }
        }
    }
}
