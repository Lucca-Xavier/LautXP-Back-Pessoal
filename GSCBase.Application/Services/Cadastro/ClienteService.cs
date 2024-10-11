using GSCBase.Application.IServices.Cadastro;
using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;

namespace GSCBase.Application.Services.Cadastro
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        private readonly IClienteRepository repository;
        public ClienteService(IClienteRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}
