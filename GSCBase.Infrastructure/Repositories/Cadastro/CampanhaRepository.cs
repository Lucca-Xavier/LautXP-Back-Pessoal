using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using GSCBase.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Infrastructure.Repositories.Cadastro
{
    public class CampanhaRepository : BaseRepository<Campanha>, ICampanhaRepository
    {
        protected Context context;

        public CampanhaRepository(Context context) : base(context)
        {
            this.context = context;
        }

    }
}
