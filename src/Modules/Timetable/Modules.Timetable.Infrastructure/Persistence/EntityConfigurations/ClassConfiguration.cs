using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Timetable.Core.Entities;

namespace Modules.Timetable.Infrastructure.Persistence.EntityConfigurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.Navigation(c => c.Audience).AutoInclude();
            builder.Navigation(c => c.Teacher).AutoInclude();
            builder.Navigation(c => c.Course).AutoInclude();
        }
    }
}