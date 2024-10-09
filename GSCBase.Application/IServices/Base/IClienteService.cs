using GSCBase.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Application.IServices.Base
{
    public interface IClienteService : IBaseService<Cliente>
    {
        Cliente FindClienteCompleto(int idCliente, int idUnidade, int idModulo);

        public ICollection<Cliente> Get(int idUnidade, int idModulo);
    }
}
