using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class PessoaMap : BaseModelMap<Pessoa>
    {
        public override void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.Property(m => m.Nome);
            builder.Property(m => m.Rzsocial);
            builder.Property(m => m.Celular);
            builder.Property(m => m.Telefone);
            builder.Property(m => m.Email);
            builder.Property(m => m.Cep);

            builder.Property(m => m.Estado); 
            builder.Property(m => m.Cidade); 
            builder.Property(m => m.Bairro); 

            builder.Property(m => m.Logradouro);
            builder.Property(m => m.Numero);
            builder.Property(m => m.Complemento);
            builder.Property(m => m.Referencia);

            builder.Property(m => m.Latitude);
            builder.Property(m => m.Longitude);
            builder.Property(m => m.Instagram);
            builder.Property(m => m.Facebook);
            builder.Property(m => m.Linkedin);

            base.Configure(builder);
        }
    }
}
