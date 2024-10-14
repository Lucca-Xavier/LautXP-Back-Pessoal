using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Infrastructure.Mapping.Cadastro
{
    public class VendaMap : BaseModelMap<Venda>
    {
        public override void Configure(EntityTypeBuilder<Venda> builder)
        {

            builder.Property(m => m.Quantidade);


            builder.HasOne(m => m.Produto)
                .WithMany()
                .HasForeignKey(m => m.IdProduto);

            builder.HasOne(m => m.Cliente)
                .WithMany()
                .HasForeignKey(m => m.IdCliente);

            base.Configure(builder);
        }
    }
}
