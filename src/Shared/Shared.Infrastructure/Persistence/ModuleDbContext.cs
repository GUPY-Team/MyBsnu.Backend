using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain;
using Shared.Core.Interfaces;
using SmartEnum.EFCore;

namespace Shared.Infrastructure.Persistence
{
    public abstract class ModuleDbContext : DbContext, IModuleDbContext
    {
        protected abstract string SchemaName { get; }

        protected ModuleDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.ConfigureSmartEnum();

            base.OnModelCreating(modelBuilder);
        }
    }
}