using Shared.Core.Domain;

namespace Shared.Core.Helpers
{
    public static class Guard
    {
        public static void RequireEntityNotNull<TEntity>(TEntity entity) where TEntity : IEntity
        {
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name);
            }
        }
    }
}