﻿using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using GSCBase.Infrastructure.Repositories.Base;

namespace GSCBase.Infrastructure.Repositories.Cadastro
{
    public class PublicidadeRepository : BaseRepository<Publicidade>, IPublicidadeRepository

    {
        protected Context context;

        public PublicidadeRepository(Context context) : base(context)
        {
            this.context = context;     
        }
    }
}
