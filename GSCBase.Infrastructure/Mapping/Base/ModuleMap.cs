using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Mapping.Base
{
    public class ModuleMap : BaseModelMap<Module>
    {
        public override void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.Property(m => m.NmModule);
            builder.Property(m => m.SgModule);
            base.Configure(builder);
        }
    }
}
