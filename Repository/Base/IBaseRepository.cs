using Model.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Repository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> Add(TEntity item);

        Task<bool> AddRange(IEnumerable<TEntity> items);

        IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, int take, int skip);

        Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, int take, int skip);

        Task<int> Count(Expression<Func<TEntity, bool>> expression);

        Task<IList<TEntity>> GetAll();

        Task<TEntity> Update(Expression<Func<TEntity, bool>> expression, TEntity item);

        Task<TEntity> Get(int id);

        Task<TEntity> Get(int id, Expression<Func<TEntity, object>> includeExpression);


        Task<int> Remove(TEntity item);

        Task<bool> DeleteById(int id, bool isSoftDeletion = false);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression);
    }
}