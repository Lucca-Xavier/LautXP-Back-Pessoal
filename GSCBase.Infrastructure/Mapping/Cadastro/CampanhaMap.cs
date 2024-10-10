using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Infrastructure.Mapping.Cadastro
{
    public class CampanhaMap : BaseModelMap<Campanha>
    {

        public override void Configure(EntityTypeBuilder<Campanha> builder)
        {

            builder.Property(m => m.Nome).IsRequired();
            builder.Property(m => m.Inicio).IsRequired();
            builder.Property(m => m.Fim).IsRequired();

            base.Configure(builder);

        }


    }
}
