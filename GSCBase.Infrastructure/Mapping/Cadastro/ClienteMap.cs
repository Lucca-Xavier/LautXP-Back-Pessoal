using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSCBase.Infrastructure.Mapping.Cadastro
{
    public class ClienteMap : BaseModelMap<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(m => m.Nome);
            builder.Property(m => m.Cpf);
            builder.Property(m => m.Pontos);
            builder.Property(m => m.Nascimento);

            base.Configure(builder);
        }
    }
}
