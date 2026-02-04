using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public static class IQueryablePagingExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query , IPagedRequest request)
        {
            var skip = request.Skip < 0 ? 0 : request.Skip;
            var take = request.Take <= 0 ? PagedRequest.DefaultTake : Math.Min(request.Take, PagedRequest.MaxTake);

            return query.Skip(skip).Take(take);
        }

        public static async Task<PagedResult<T>> PageResultAsync<T>(
            this IQueryable<T> query, 
            IPagedRequest request,
            CancellationToken cancellationToken = default)
        {
            var total = await query.CountAsync(cancellationToken);
            var items = await query.ApplyPaging(request).ToListAsync(cancellationToken);
            return new PagedResult<T>(items, total,request.Skip,request.Take);
        }

    }
}
