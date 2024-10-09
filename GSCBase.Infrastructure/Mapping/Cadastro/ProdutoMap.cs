using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSCBase.Infrastructure.Mapping.Cadastro
{
    public class ProdutoMap : BaseModelMap<Produto>
    {
        public override void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(m => m.Rotulo);
            builder.Property(m => m.Tamanho);

            base.Configure(builder);
        }
    }
}
