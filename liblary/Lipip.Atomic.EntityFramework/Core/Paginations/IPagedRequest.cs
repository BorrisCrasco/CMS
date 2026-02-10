namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public interface IPagedRequest
    {
        int PageNumber { get; }
        int PageSize { get; }
    }
}