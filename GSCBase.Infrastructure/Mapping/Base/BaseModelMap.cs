using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;

namespace GSCBase.Infrastructure.Mappings.Base
{
    public abstract class BaseModelMap<T> where T : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.IsActive);
            builder.Property(m => m.DtCreate);
            builder.Property(m => m.DtAlter);
            builder.Property(m => m.DtDeleted);

            builder.HasOne(m => m._UserCreate)
                .WithMany()
                .HasForeignKey(m => m.UserCreate);
            builder.HasOne(m => m._UserAlter)
                .WithMany()
                .HasForeignKey(m => m.UserAlter);
            builder.HasOne(m => m._UserDeleted)
                .WithMany()
                .HasForeignKey(m => m.UserDeleted);
        }
    }
}

