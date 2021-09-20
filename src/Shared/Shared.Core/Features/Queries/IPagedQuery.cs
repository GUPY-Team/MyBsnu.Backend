namespace Shared.Core.Features.Queries
{
    public interface IPagedQuery
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}