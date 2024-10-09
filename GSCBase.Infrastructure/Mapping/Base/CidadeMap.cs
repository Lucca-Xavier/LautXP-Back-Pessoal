using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class CidadeMap
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome);

            builder.HasOne(m => m.Estado)
                .WithMany()
                .HasForeignKey(m => m.IdEstado);

        }
    }
}
