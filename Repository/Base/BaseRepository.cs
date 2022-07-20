using Microsoft.EntityFrameworkCore;
using Model;
using Model.Base;
using Repository.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
    {

        protected readonly DbSet<TEntity> _dbSet;
        protected EDbContext dbContext;
        public BaseRepository(EDbContext _dbContext)
        {
            dbContext = _dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public virtual async Task<TEntity> Add(TEntity item)
        {
            try
            {
                var result = await dbContext.Set<TEntity>().AddAsync(item);
                await dbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual async Task<bool> AddRange(IEnumerable<TEntity> list)
        {
            try
            {
                await dbContext.Set<TEntity>().AddRangeAsync(list);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, int take, int skip)
        {
            var query = dbContext.Set<TEntity>().AsNoTracking();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query.Skip(skip).Take(take);
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> expression)
        {
            var query = dbContext.Set<TEntity>();
            if (expression != null)
            {
                return await query.Where(expression).AsNoTracking().CountAsync();
            }

            return await query.AsNoTracking().CountAsync();
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await dbContext.Set<TEntity>().AsNoTracking()?.FirstOrDefaultAsync(whereExpression);
        }

        public virtual async Task<TEntity> Get(int id, Expression<Func<TEntity, object>> includeExpression)
        {
            if (includeExpression is null)
            {
                return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            }
            return await dbContext.Set<TEntity>().AsNoTracking()?.Include(includeExpression)?
                .FirstOrDefaultAsync(x => x.id == id);
        }

        public virtual async Task<IList<TEntity>> GetAll()
        {
            var query = dbContext.Set<TEntity>();
            return await query?.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, int take, int skip)
        {
            return await this.All(expression, take, skip)?.ToListAsync();
        }

        //public virtual async Task<SearchResult<TEntity>> Search(int take, int currentPage, IList<Sort> sortList = null, IList<Filter> filters = null)
        //{
        //    //using (var transaction = dbContext.Database.BeginTransaction())
        //    //{
        //        Expression<Func<T, bool>> expression = filters.ToConditionExpression<TEntity>();
        //        var query = dbContext.Set<TEntity>();
        //        var temp = query.Where(expression);
        //        var results = temp;

        //        if (sortList != null)
        //        {
        //            foreach (var sort in sortList)
        //            {
        //                results = OrderBy(results, sort);
        //            }
        //        }
        //        var skip = (currentPage - 1) * take;
        //        results = results.Skip(skip).Take(take);
        //        var count =await temp.CountAsync();
        //        var searchResult = new SearchResult<TEntity>();
        //        var pager = new Pager
        //        {
        //            CurrentPage = currentPage,
        //            NumberPerPage = take,
        //            TotalItems = count,
        //            TotalPages = (count + take - 1) / take
        //        };

        //        searchResult.Data = results.ToList();
        //        searchResult.Pager = pager;
        //        return searchResult;
        //    //}
        //}

        //protected IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> source, Sort sort)
        //{
        //    var type = typeof(T);
        //    var property = type.GetProperty(sort.ColumnName);
        //    var parameter = Expression.Parameter(type, "p");
        //    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //    var orderByExp = Expression.Lambda(propertyAccess, parameter);
        //    MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
        //        sort.SortOrder == SortOrder.Asc ? "OrderBy" : "OrderByDescending",
        //        new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
        //    return source.Provider.CreateQuery<TEntity>(resultExp);
        //}

        public virtual async Task<TEntity> Update(Expression<Func<TEntity, bool>> expression, TEntity item)
        {
            try
            {
                var query = dbContext.Set<TEntity>().AsNoTracking();
                var found = query.FirstOrDefault(expression);
                item.id = found.id;
                dbContext.Set<TEntity>().Attach(item);
                dbContext.Set<TEntity>().Update(item);
                await dbContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual async Task<int> Remove(TEntity item)
        {
            dbContext.Set<TEntity>().Remove(item);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveRange(IQueryable<TEntity> items)
        {
            dbContext.Set<TEntity>().RemoveRange(items);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteById(int id, bool isSoftDeletion = false)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var item = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.id == id);
                    if (item == null)
                    {
                        return false;
                    }
                    int result = 0;
                    if (!isSoftDeletion)
                    {
                        result = await this.Remove(item);
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        //item.IsDeleted = true;
                        //result = (await Update(item)).Entity.IsDeleted ? 1 : 0;
                    }
                    transaction.Commit();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            var query = dbContext.Set<TEntity>().AsNoTracking();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }
    }
}