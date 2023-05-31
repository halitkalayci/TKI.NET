using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.InMemory
{
    public class InMemoryCarRepository : ICarRepository
    {
        public List<Car> cars = new List<Car>();
        public void Add(Car car)
        {
            cars.Add(car);
        }

        public void Delete(int id)
        {
            Car carToRemove = GetById(id);
            cars.Remove(carToRemove);
        }

        public List<Car> GetAll()
        {
            return cars;
        }

        public Car GetById(int id)
        {
            // LINQ 
            // Lambda Expression
            // cars.Where(i => i.Id == id).OrderBy(i=>i.Id).FirstOrDefault();
            return cars.FirstOrDefault(i=>i.Id == id);
        }

        public Car GetByPlate(string plate)
        {
            return cars.FirstOrDefault(i => i.Plate == plate);
        }

        public void Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
