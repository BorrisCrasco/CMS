using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public sealed class PagedRequest : IPagedRequest
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 5000;

        public int PageNumber { get; }
        public int PageSize { get; }

        internal int Skip => (PageNumber - 1) * PageSize;
        internal int Take => PageSize;

        public PagedRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0
                ? DefaultPageSize
                : Math.Min(pageSize, MaxPageSize);

        }

        public static PagedRequest From(int? pageNumber, int? pageSize)
            => new(pageNumber ?? 1, pageSize ?? DefaultPageSize);

    }
}
