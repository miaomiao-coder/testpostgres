
using AutoMapper;
using Model.Base;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public class ServiceBase<T> : IServiceBase<T> where T : EntityBase
    {
        protected readonly IMapper Mapper;
        protected IBaseRepository<T> Repository { get; set; }

        public ServiceBase(IMapper mapper, IBaseRepository<T> repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public virtual async Task<TResult> Add<TInput, TResult>(TInput item)
        {
            var domainModel = Mapper.Map<TInput, T>(item);
            var result = await Repository.Add(domainModel);
            return Mapper.Map<T, TResult>(result);
        }
        public virtual async Task<TResult> Add<TResult>(T item)
        {
            var result = await Repository.Add(item);
            return Mapper.Map<T, TResult>(result);
        }

        public virtual async Task<IList<TResult>> GetAll<TResult>(Expression<Func<T, bool>> expression, int take,
            int skip)
        {
            var result = await Repository.GetAll(expression, take, skip);
            return Mapper.Map<IList<T>, IList<TResult>>(result);

        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return await Repository.Count(expression);
        }

        public virtual async Task<IList<TResult>> GetAll<TResult>()
        {
            var result = await Repository.GetAll();
            return Mapper.Map<IList<T>, IList<TResult>>(result);
        }

        public virtual async Task<TResult> Update<TInput, TResult>(Expression<Func<T, bool>> expression, TInput item)
        {
            var domainModel = Mapper.Map<TInput, T>(item);
            var result = await Repository.Update(expression, domainModel);
            return Mapper.Map<T, TResult>(result);
        }

        public virtual async Task<TResult> Get<TResult>(Expression<Func<T, bool>> whereExpression)
        {
            var result = await Repository.Get(whereExpression);
            return Mapper.Map<T, TResult>(result);
        }

        public virtual async Task<TResult> Get<TResult>(int id)
        {
            var result = await Repository.Get(id);
            if (result is null)
            {
                return default(TResult);
            }
            return Mapper.Map<T, TResult>(result);
        }

        public virtual async Task<TResult> Get<TResult>(int id, Expression<Func<T, object>> includeExpression = null)
        {
            var result = await Repository.Get(id, includeExpression);
            return Mapper.Map<T, TResult>(result);
        }

        public virtual async Task<int> Remove(T item)
        {
            return await Repository.Remove(item);
        }

        public virtual async Task<bool> DeleteById(int id, bool isSoftDeletion = false)
        {
            return await Repository.DeleteById(id, isSoftDeletion);
        }

    }
}
