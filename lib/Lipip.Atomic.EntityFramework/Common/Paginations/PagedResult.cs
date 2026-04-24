using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Common.Paginations
{
    public sealed class PagedResult<T>
    {

        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public PagedResult(
            IReadOnlyList<T> items,int totalCount,int pageNumber,int pageSize)
        {
            Items = items is IReadOnlyList<T> list ? list : items.ToList();
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }



    }
}
