namespace Lipip.Atomic.EntityFramework.Common.Paginations
{
    public interface IPagedRequest
    {
        int PageNumber { get; }
        int PageSize { get; }
    }
}