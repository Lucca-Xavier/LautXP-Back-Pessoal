using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    class EstadoMap
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
           
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome);
            builder.Property(m => m.Sigla);
               
        }
    }
}
