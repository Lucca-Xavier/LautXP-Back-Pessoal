using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSCBase.Infrastructure.Mapping.Cadastro
{
    public class PublicidadeMap : BaseModelMap<Publicidade>
    {
        public override void Configure(EntityTypeBuilder<Publicidade> builder)
        {

            builder.Property(m => m.Arquivo);
            builder.Property(m => m.Formato);

            base.Configure(builder);
        }
        
    }
}
