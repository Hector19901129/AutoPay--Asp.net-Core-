using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoPay.Infrastructure.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace AutoPay.DataLayer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private DbSet<T> DbSet()
        {
            return _dataContext.Set<T>();
        }

        public IQueryable<T> Entity(bool trackEntity = false)
        {
            return trackEntity ? DbSet() : DbSet().AsNoTracking();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var dbSet = DbSet().Where(predicate).AsQueryable();

            if (includes == null || !includes.Any()) return dbSet;

            return includes.Aggregate(dbSet, (current, include) => current.Include(include));
        }

        public T Find(params object[] keys)
        {
            return DbSet().Find(keys);
        }

        public async Task<T> FindAsync(params object[] keys)
        {
            return await DbSet().FindAsync(keys);
        }

        public T Find(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            if (includes == null || !includes.Any()) return DbSet().SingleOrDefault(predicate);

            foreach (var include in includes)
            {
                DbSet().Include(include);
            }

            return DbSet().SingleOrDefault();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            if (includes == null || !includes.Any()) return await DbSet().SingleOrDefaultAsync(predicate);

            var dbSet = DbSet().AsQueryable();

            foreach (var include in includes)
            {
                dbSet = DbSet().Include(include);
            }

            return await dbSet.SingleOrDefaultAsync();
        }

        public T Insert(T entity)
        {
            DbSet().Add(entity);

            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            await DbSet().AddAsync(entity);

            return entity;
        }

        public IEnumerable<T> InsertMany(ICollection<T> entities)
        {
            DbSet().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> InsertManyAsync(ICollection<T> entities)
        {
            await DbSet().AddRangeAsync(entities);

            return entities;
        }

        public void Update(T entity)
        {
            DbSet().Attach(entity);

            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbSet().Remove(entity);
        }
    }
}
