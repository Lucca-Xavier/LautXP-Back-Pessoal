using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GSCBase.Infrastructure.Repositories.Base
{
    public class CommonRepository : ICommonRepository
    {
        protected Context context;
        public CommonRepository(Context ctx) 
        {
            context = ctx;
        }

        public List<Bairro> ListarBairros()
        {
            List<Bairro> bairros = context.Bairro
                .OrderBy(x => x.Nome).ToList();

            return bairros;
        }

        public List<Bairro> ListarBairros(int? idCidade)
        {
            List<Bairro> bairros = context.Bairro
                .Where(x => x.IdCidade == idCidade)
                .OrderBy(x => x.Nome).ToList();

            return bairros;
        }

        public List<Cidade> ListarCidades()
        {
            List<Cidade> cidades = context.Cidade
                .OrderBy(x => x.Nome).ToList();

            return cidades;
        }

        public List<Cidade> ListarCidades(int? idEstado)
        {
            List<Cidade> cidades = context.Cidade
                .Where(x => x.IdEstado == idEstado)
                .OrderBy(x => x.Nome).ToList();

            return cidades;
        }

        public List<Estado> ListarEstados()
        {
            List<Estado> estados = context.Estado
                .OrderBy(x => x.Nome).ToList();

            return estados;
        }

        public Bairro ObterBairroPorId(int? idBairro)
        {
            Bairro bairro = context.Bairro
                .FirstOrDefault(x => x.Id == idBairro);
            return bairro;
        }

        public Cidade ObterCidadePorId(int? idCidade)
        {
            Cidade cidade = context.Cidade
                .FirstOrDefault(x => x.Id == idCidade);
            return cidade;
        }

        public Estado ObterEstadoPorId(int? idEstado)
        {
            Estado estado = context.Estado
                .FirstOrDefault(x => x.Id == idEstado);
            return estado;
        }

    }
}
