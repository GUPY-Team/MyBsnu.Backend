using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Timetable.Core.Entities;

namespace Modules.Timetable.Infrastructure.Persistence.EntityConfigurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.Navigation(c => c.Audiences).AutoInclude();
            builder.Navigation(c => c.Teachers).AutoInclude();
            builder.Navigation(c => c.Course).AutoInclude();

            builder.HasMany(c => c.Teachers)
                .WithMany(t => t.Classes);

            builder.HasMany(c => c.Audiences)
                .WithMany(a => a.Classes);
        }
    }
}