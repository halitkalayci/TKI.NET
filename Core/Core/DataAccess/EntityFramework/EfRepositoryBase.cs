using Core.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private TContext Context;
        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
            Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            // SingleOrDefault => Filtre ile bir veriye değil de birden fazla veriye ulaşırsa exception fırlatır.
            // FirstOrDefault =>  Filtrelenen veri setinin büyüklüğü önemsiz, bulduğu (varsa) ilk değeri döner.
            IQueryable<TEntity> result = Context.Set<TEntity>();
            if (include != null)
                result = include(result);
            if (filter != null)
                result = result.Where(filter);

            return result.FirstOrDefault();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            // ternary operator => ?
            // {koşul} ? {true} : {false}

            IQueryable<TEntity> queryable = Context.Set<TEntity>();
            if (include != null)
                queryable = include(queryable);
            if (filter != null)
                queryable = queryable.Where(filter);
            return queryable.ToList();
        }

        public void Update(TEntity entity)
        {
            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
