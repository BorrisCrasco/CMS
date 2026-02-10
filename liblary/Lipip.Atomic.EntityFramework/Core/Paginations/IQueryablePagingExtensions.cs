using Microsoft.EntityFrameworkCore;

namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public static class IQueryablePagingExtensions
    {
        public static async Task<PagedResult<T>> PageResultAsync<T>( this IQueryable<T> query,
            PagedRequest request,  CancellationToken cancellationToken = default)
        {
            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(
                items,
                total,
                request.PageNumber,
                request.PageSize
            );
        }

        public static PagedResult<T> PageResult<T>(this IQueryable<T> query,PagedRequest request)
        {
            var total = query.Count();

            var items = query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            return new PagedResult<T>(
                items,
                total,
                request.PageNumber,
                request.PageSize
            );
        }
    }
}
