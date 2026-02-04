using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Atomics
{
    public class AtomicDbContextProxy<TContext> where TContext : DbContext
    {
        private readonly TContext _inner;

        public AtomicDbContextProxy(TContext inner)
        {
            _inner = inner;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
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
    }

}
