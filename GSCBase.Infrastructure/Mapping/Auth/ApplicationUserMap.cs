using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Auth;

namespace GSCBase.Infrastructure.Mappings
{
    public class ApplicationUserMap
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Email);
            builder.Property(m => m.EmailConfirmed);
            builder.Property(m => m.PasswordHash);
            builder.Property(m => m.SecurityStamp);
            builder.Property(m => m.PhoneNumber);
            builder.Property(m => m.PhoneNumberConfirmed);
            builder.Property(m => m.TwoFactorEnabled);
            builder.Property(m => m.LockoutEnd);
            builder.Property(m => m.LockoutEnabled);
            builder.Property(m => m.AccessFailedCount);
            builder.Property(m => m.NormalizedEmail);
            builder.Property(m => m.NormalizedUserName);
            builder.Property(m => m.ConcurrencyStamp);
            builder.Property(m => m.IsActive);
            builder.Property(m => m.DtCreate);
            builder.Property(m => m.UserCreate);
            builder.Property(m => m.DtAlter);
            builder.Property(m => m.UserAlter);
            builder.Property(m => m.DtDeleted);
            builder.Property(m => m.UserDeleted);
            builder.Property(m => m.Nome);
            builder.HasOne(m => m.Pessoa)
                .WithMany()
                .HasForeignKey(m => m.IdPessoa);
            builder.HasOne(m => m.TipoUsuario)
                .WithMany()
                .HasForeignKey(m => m.IdTipoUsuario);

    }
    }
}
