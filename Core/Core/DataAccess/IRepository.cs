using Core.Entities.Abstracts;
using Core.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        // Default Parameter
        List<T> GetAll(Expression<Func<T, bool>>? filter= null, Func<IQueryable<T>, IIncludableQueryable<T,object>>? include = null);
        // GetAll()
        // GetAll(i=>i.Id==id)
        T Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        // Get()
        // Get(i=>i.Id==id)
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

// ICarRepository : IRepository<Car>
