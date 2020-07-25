using AVB.ApiBlog.DataAccess.EntityFrameworkCore;
using AVB.ApiBlog.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AVB.ApiBlog.DataAccess.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public async Task<List<TEntity>> GetAll()
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> GetWithLimit(int limit)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().Take(limit).ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task Add(TEntity entity)
        {
            await using var context = new DatabaseContext();
            var e = context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            await using var context = new DatabaseContext();
            context.Update(entity);
            //context.Update(entity);
            return await context.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            await using var context = new DatabaseContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}