using GSCBase.Application.IServices.Cadastro;
using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;

namespace GSCBase.Application.Services.Cadastro
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        private readonly IProdutoRepository repository;
        public ProdutoService(IProdutoRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
