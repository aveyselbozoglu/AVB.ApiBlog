using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AVB.ApiBlog.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity, bool>> filter);

        Task<List<TEntity>> GetWithLimit(int limit);

        Task<TEntity> GetById(int id);

        Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);

        Task Add(TEntity entity);

        Task<int> Update(TEntity entity);

        Task Remove(TEntity entity);
    }
}