using GSCBase.Application.IServices.Cadastro;
using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;

namespace GSCBase.Application.Services.Cadastro
{
    public class PublicidadeService : BaseService<Publicidade>, IPublicidadeService
    {
        private readonly IPublicidadeRepository repository;

        public PublicidadeService(IPublicidadeRepository repository) : base(repository)
        {
            this.repository = repository;

        }

    }

}
