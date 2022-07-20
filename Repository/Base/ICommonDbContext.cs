
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Base
{
    public interface ICommonDbContext : IDisposable
    {

        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;


        IDbContextTransaction BeginTransaction();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void SetEntry<TEntity>(TEntity entity, EntityState entityState) where TEntity : class;

        void SetEntry(object entity, EntityState entityState);


    }
}
