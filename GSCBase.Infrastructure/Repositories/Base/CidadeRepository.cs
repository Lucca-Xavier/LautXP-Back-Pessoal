using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class CidadeRepository : BaseRepository<Cidade>, ICidadeRepository    
    {
        public CidadeRepository(Context ctx) : base(ctx)
        {

        }
    }
}
