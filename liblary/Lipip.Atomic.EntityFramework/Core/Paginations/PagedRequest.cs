using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public sealed class PagedRequest : IPagedRequest
    {
        public const int DefaultTake = 10;
        public const int MaxTake = 5000;

        public int Skip { get; init; }
        public int Take { get; init; } = DefaultTake;

        public PagedRequest()
        {
            
        }

        public PagedRequest(int skip, int take)
        {
            Skip = skip < 0 ? 0 : skip;
            Take = take <= 0 ? DefaultTake : Math.Min(take, MaxTake);

        }

        public static PagedRequest From(int? skip, int? take) =>
            new(skip ?? 0, take ?? DefaultTake);

    }
}
