using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Application.Services.Base
{
    public class TipoUsuarioService : BaseService<TipoUsuario>, ITipoUsuarioService
    {

        public TipoUsuarioService(ITipoUsuarioRepository repository) : base(repository)
        {

        }
    }
}
