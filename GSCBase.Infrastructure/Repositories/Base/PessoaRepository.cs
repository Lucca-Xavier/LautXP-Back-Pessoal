using Microsoft.EntityFrameworkCore;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {
        protected Context context;
        public PessoaRepository(Context ctx) : base(ctx)
        {
            context = ctx;
        }

        public Pessoa GetPessoaById(int id)
        {
            return context.Pessoa.FirstOrDefault(x => x.Id == id);
        }
    }
}
