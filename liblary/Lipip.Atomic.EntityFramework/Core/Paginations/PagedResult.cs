using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public sealed class PagedResult<T>
    {

        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int Skip { get; }
        public int Take { get; }

        public bool HasPrevious => Skip > 0;
        public bool HasNext => Skip + Take < TotalCount;


        public PagedResult(IReadOnlyList<T> items, int totalCount, int skip, int take)
        {
            Items = items is IReadOnlyList<T> list ? list : items.ToList();
            TotalCount = totalCount;
            Skip = skip;
            Take = take;
        }



    }
}
