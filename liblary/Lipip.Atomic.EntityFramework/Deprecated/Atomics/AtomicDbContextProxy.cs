using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Lipip.Atomic.EntityFramework.Common.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Deprecated.Atomics
{
    [Obsolete]
    public class AtomicDbContextProxy<TContext> where TContext : DbContext
    {
        private readonly TContext _inner;
        private readonly ICurrentUser _currentUser;

        public AtomicDbContextProxy(TContext inner, ICurrentUser currentUser)
        {
            _inner = inner;
            _currentUser = currentUser;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAutomaticAudit();

            if (_inner.Database.CurrentTransaction == null)
            {
                await using var transaction = await _inner.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var result = await _inner.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return await _inner.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class => _inner.Set<TEntity>();

        public TContext Db => _inner;

        private void ApplyAutomaticAudit()
        {
            var entries = _inner.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted);

            var now = DateTime.UtcNow;
            var user = _currentUser.UserId ?? "SYSTEM";

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var type = entity.GetType();

                var createdDateProp = type.GetProperty("CreatedDate");
                var createdByProp = type.GetProperty("CreatedBy");
                var updatedDateProp = type.GetProperty("UpdatedDate");
                var updatedByProp = type.GetProperty("UpdatedBy");
                var isDeletedProp = type.GetProperty("IsDeleted");
                var deletedDateProp = type.GetProperty("DeletedDate");
                var deletedByProp = type.GetProperty("DeletedBy");

                if (entry.State == EntityState.Added)
                {
                    createdDateProp?.SetValue(entity, now);
                    createdByProp?.SetValue(entity, user);
                }

                if (entry.State == EntityState.Modified)
                {
                    updatedDateProp?.SetValue(entity, now);
                    updatedByProp?.SetValue(entity, user);
                }

                if (entry.State == EntityState.Deleted && isDeletedProp != null)
                {
                    entry.State = EntityState.Modified;

                    isDeletedProp.SetValue(entity, true);
                    deletedDateProp?.SetValue(entity, now);
                    deletedByProp?.SetValue(entity, user);
                }
            }
        }
    }





}


