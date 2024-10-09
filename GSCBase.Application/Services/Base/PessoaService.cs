using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.IRepositories.Base;
using GSCBase.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Application.Services.Base
{
    public class PessoaService : BaseService<Pessoa>, IPessoaService
    {
        private readonly IPessoaRepository repository;
        public PessoaService(IPessoaRepository _repository) : base(_repository)
        {
            repository = _repository;
        }
    }
}
