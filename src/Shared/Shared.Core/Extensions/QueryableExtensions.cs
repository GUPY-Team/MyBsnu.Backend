using System.Linq;

namespace Shared.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            return queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}