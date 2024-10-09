using Microsoft.EntityFrameworkCore;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Auth;
using GSCBase.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSCBase.Infrastructure.Repositories.Auth
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly Context context;
        public ApplicationUserRepository(Context ctx) : base(ctx)
        {
            context = ctx;
        }

        public override IQueryable<ApplicationUser> Get()
        {
            return context.ApplicationUser.Include(m => m.TipoUsuario);
        }
    }
}
