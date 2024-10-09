using GSCBase.Domain.Entities.Base;

namespace GSCBase.Infrastructure.IRepositories.Base
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        Pessoa GetPessoaById(int id);
    }
}
