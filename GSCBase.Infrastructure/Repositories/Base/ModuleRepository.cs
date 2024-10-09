using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(Context ctx) : base(ctx)
        {

        }
    }
}
