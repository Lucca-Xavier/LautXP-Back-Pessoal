using Microsoft.EntityFrameworkCore;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        Context context;
        public ClienteRepository(Context ctx) : base(ctx)
        {
            context = ctx;
        }
    }
}
