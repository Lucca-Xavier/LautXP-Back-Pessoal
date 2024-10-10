using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using GSCBase.Infrastructure.Repositories.Base;

namespace GSCBase.Infrastructure.Repositories.Cadastro
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        protected Context context;
        public ClienteRepository(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
