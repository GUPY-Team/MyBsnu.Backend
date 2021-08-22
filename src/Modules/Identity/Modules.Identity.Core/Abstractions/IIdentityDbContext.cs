using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Entities;
using Shared.Core.Interfaces;

namespace Modules.Identity.Core.Abstractions
{
    public interface IIdentityDbContext : IDbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
    }
}