using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using GSCBase.Infrastructure.Repositories.Base;

namespace GSCBase.Infrastructure.Repositories.Cadastro
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        protected Context context;
        public ProdutoRepository(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
