using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class BairroRepository : BaseRepository<Bairro>, IBairroRepository
    {
        public BairroRepository(Context ctx) : base(ctx)
        {

        }
    }
}
