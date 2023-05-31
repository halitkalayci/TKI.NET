using Core.Entities.Abstracts;
using Core.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    // CRUD Operasyonu
    // Create
    // Read
    // Update
    // Delete

    // constraints
    //TODO: Refactor
    // type

    // XRepository : IRepository  !! Yanlış
    // XRepository : IRepository<X> Doğru
    // IRepository<Program>
    public interface IRepository<T> where T : IEntity, new() 
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}

// ICarRepository : IRepository<Car>
