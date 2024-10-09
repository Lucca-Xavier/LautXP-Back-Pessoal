using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class ClienteMap : BaseModelMap<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(m => new { m.Id });
            builder.HasOne(m => m.Pessoa)
                .WithMany()
                .HasForeignKey(m => m.Id);
            builder.Property(m => m.GatewayContact);
            base.Configure(builder);
        }
    }
}
