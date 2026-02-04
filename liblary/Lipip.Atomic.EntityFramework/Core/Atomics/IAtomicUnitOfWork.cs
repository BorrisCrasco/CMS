using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Atomics
{
    public interface IAtomicUnitOfWork
    {
        Task CommitAsync();
    }
}
