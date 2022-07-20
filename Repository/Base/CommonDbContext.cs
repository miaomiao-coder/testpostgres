
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Base
{

    public class CommonDbContext : DbContext, ICommonDbContext
    { 
        public CommonDbContext(DbContextOptions options) : base(options)
        {
        }


        private readonly string _govCode;
  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
#else
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Error);
#endif
            base.OnConfiguring(optionsBuilder);
        }

        public virtual new void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableEntityType> deleteEntities = EntityPropertiesCache.GetEntitiesWithIDeleteEntity(modelBuilder);
            //IEnumerable<IMutableEntityType> govEntities = EntityPropertiesCache.GetEntitiesWithIGovEntity(modelBuilder);
            //IEnumerable<IMutableEntityType> govermentEntities = EntityPropertiesCache.GetEntitiesWithIGovermentEntites(modelBuilder);

            //if (deleteEntities != null && deleteEntities.Any())
            //{
            //    foreach (IMutableEntityType entityType in deleteEntities)
            //    {
            //        modelBuilder.Entity(entityType.ClrType).Property<Boolean>("IsDeleted");
            //        var parameter = Expression.Parameter(entityType.ClrType, "e");
            //        var body = Expression.Equal(
            //            Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
            //        Expression.Constant(false));
            //        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            //    }
            //}

            //if (govEntities != null && govEntities.Any())
            //{
            //    foreach (IMutableEntityType entityType in govEntities)
            //    {
            //        modelBuilder.Entity(entityType.ClrType).Property<string>("GovCode");
            //        var parameter = Expression.Parameter(entityType.ClrType, "e");
            //        var body = Expression.Equal(
            //            Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(string) }, parameter, Expression.Constant("GovCode")),
            //        Expression.Constant(_govCode));
            //        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            //    }
            //}
             
            HandlerProperties(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void HandlerProperties(ModelBuilder modelBuilder)
        {
            Dictionary<Type, Dictionary<string, string>> dicEntityPropertyTypeCache = EntityPropertiesCache.GetEntityPropertyTypeCache();
            foreach (var dic in dicEntityPropertyTypeCache)
            {
                foreach (var dicP in dic.Value)
                {
                    modelBuilder.Entity(dic.Key).Property(dicP.Key).HasColumnType(dicP.Value);
                }
            }
        }

        public void SetEntry<TEntity>(TEntity entity, EntityState entityState) where TEntity : class
        {
            this.Entry<TEntity>(entity).State = entityState;
        }

        public void SetEntry(object entity, EntityState entityState)
        {
            this.Entry(entity).State = entityState;
        }



        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库操作异常", ex);
            }

        }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                //  var changes = this.ChangeTracker.Entries();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("数据库操作异常", ex);
            }

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }


    }
}
