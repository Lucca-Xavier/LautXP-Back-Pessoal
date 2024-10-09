using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class TipoUsuarioMap : BaseModelMap<TipoUsuario>
    {
        public override void Configure(EntityTypeBuilder<TipoUsuario> builder) 
        {
            builder.Property(m => m.Nome);
            base.Configure(builder);
        }
    }
}
