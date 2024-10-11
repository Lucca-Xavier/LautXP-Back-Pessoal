using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using GSCBase.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Infrastructure.Repositories.Cadastro
{
    public class VendaRepository : BaseRepository<Venda>, IVendaRepository
    {

        protected Context context;

        public VendaRepository(Context context) :base(context)
        {
            this.context = context;
        }


    }
}
