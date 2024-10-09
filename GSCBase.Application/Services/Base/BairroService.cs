using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Application.Services.Base
{
    public class BairroService : BaseService<Bairro>, IBairroService
    {
        public BairroService(IBairroRepository repository) : base(repository)
        {

        }
    }
}
