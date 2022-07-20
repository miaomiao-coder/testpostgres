using Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public interface IServiceBase<T> where T : EntityBase
    {

        Task<TResult> Add<TInput, TResult>(TInput item);

        Task<IList<TResult>> GetAll<TResult>(Expression<Func<T, bool>> expression, int take, int skip);

        Task<int> Count(Expression<Func<T, bool>> expression);

        Task<IList<TResult>> GetAll<TResult>();

        Task<TResult> Update<TInput, TResult>(Expression<Func<T, bool>> expression, TInput item);

        Task<TResult> Get<TResult>(int id);

        Task<TResult> Get<TResult>(int id, Expression<Func<T, object>> includeExpression);

        Task<int> Remove(T item);

        Task<bool> DeleteById(int id, bool isSoftDeletion);
    }
}
