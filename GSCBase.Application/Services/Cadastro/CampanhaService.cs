using GSCBase.Application.IServices.Cadastro;
using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Application.Services.Cadastro
{
    public class CampanhaService : BaseService<Campanha> ,ICampanhaService
    {

        private readonly ICampanhaRepository repository;

        public CampanhaService(ICampanhaRepository repository) : base(repository)
        {
            this.repository = repository;
        }

    }
}
