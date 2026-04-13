using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Deprecated.Atomics
{
    [Obsolete]
    public interface IAtomicUnitOfWork
    {
        Task CommitAsync();
    }
}
