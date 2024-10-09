using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSCBase.Application.Services.Base
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        public ClienteService(IClienteRepository repository) : base(repository)
        {

        }

        public Cliente FindClienteCompleto(int idCliente, int idUnidade, int idModulo)
        {
            return this.Find(m => m.Id == idCliente && m.IsActive == true);
        }

        public ICollection<Cliente> Get(int idUnidade, int idModulo)
        {
            return this.Get(m => m.IsActive == true).ToList();
        }
    }
}
