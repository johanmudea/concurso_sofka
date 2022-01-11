//repositorio de funcionalidades para interaccion con tablas.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ConcursoSofka.Data.AccessData
{
    public class Repository<T> where T : EntityBase
    {
        private BusinessContext _businessContext;
        public Repository(BusinessContext context)
        {
            _businessContext = context;
        }

        public int Commit()
        {
            return _businessContext.SaveChanges();
        }

        public EntityEntry Add(T entity)
        {
            entity.CreatedOn = DateTime.Now;
            return _businessContext.Set<T>().Add(entity);
        }

        public async Task<EntityEntry> AddAsync(T entity)
        {
            entity.CreatedOn = DateTime.Now;
            var resp = _businessContext;
            return await resp.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                item.CreatedOn = DateTime.Now;
            }
            await _businessContext.Set<T>().AddRangeAsync(entities);
        }

        public EntityEntry Delete(T entity)
        {
            return _businessContext.Set<T>().Remove(entity);
        }

        public EntityEntry Delete(object id)
        {
            T entityToDelete = _businessContext.Set<T>().Find(id);
            if (entityToDelete != null)
                return Delete(entityToDelete);
            else
                return null;
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entityToDelete in entities)
            {
                Delete(entityToDelete);
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _businessContext.Set<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetFirst(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _businessContext.Set<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public EntityEntry Update(T entity)
        {
            return _businessContext.Set<T>().Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            _businessContext.Set<T>().UpdateRange(entities);
        }
    }
}
