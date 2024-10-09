using GSCBase.Domain.Entities.Auth;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Infrastructure.IRepositories.Auth
{
    public interface IApplicationUserRepository: IBaseRepository<ApplicationUser>
    {
    }
}
