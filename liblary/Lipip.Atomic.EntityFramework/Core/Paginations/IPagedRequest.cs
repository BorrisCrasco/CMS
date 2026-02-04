namespace Lipip.Atomic.EntityFramework.Core.Paginations
{
    public interface IPagedRequest
    {
        int Skip { get; }
        int Take { get; }
    }
}