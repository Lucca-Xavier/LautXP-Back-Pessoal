using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class BairroMap
    {
        public void Configure(EntityTypeBuilder<Bairro> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome);
            builder.HasOne(m => m.Cidade)
                .WithMany()
                .HasForeignKey(m => m.IdCidade);
        }
    }
}
