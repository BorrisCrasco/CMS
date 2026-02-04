using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Atomics
{
    public class AtomicUnitOfWork<TContext> : IAtomicUnitOfWork where TContext : DbContext
    {
        private readonly AtomicDbContextProxy<TContext> _proxy;

        public AtomicUnitOfWork(AtomicDbContextProxy<TContext> proxy)
        {
            _proxy = proxy;
        }

        public Task CommitAsync() => _proxy.SaveChangesAsync();
    }

}
