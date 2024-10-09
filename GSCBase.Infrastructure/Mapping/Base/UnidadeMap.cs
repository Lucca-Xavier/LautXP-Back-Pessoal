using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class UnidadeMap : BaseModelMap<Unidade>
    {
        public override void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.HasOne(m => m.Pessoa)
                .WithMany()
                .HasForeignKey(m => m.Id);
            builder.Property(m => m.NmUnidade);
            builder.Property(m => m.Sistema);
            
            base.Configure(builder);
        }
    }
}
