using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class UnidadeRepository : BaseRepository<Unidade>, IUnidadeRepository
    {
        public UnidadeRepository(Context ctx) : base(ctx)
        {

        }
    }
}
