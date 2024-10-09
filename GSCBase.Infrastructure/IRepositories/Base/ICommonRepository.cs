using GSCBase.Domain.Entities.Base;
using System.Collections.Generic;

namespace GSCBase.Infrastructure.IRepositories.Base
{
    public interface ICommonRepository
    {
        List<Bairro> ListarBairros();
        List<Bairro> ListarBairros(int? idCidade);
        Bairro ObterBairroPorId(int? idBairro);

        List<Cidade> ListarCidades();
        List<Cidade> ListarCidades(int? idEstado);
        Cidade ObterCidadePorId(int? idCidade);

        List<Estado> ListarEstados();
        Estado ObterEstadoPorId(int? idEstado);
    }
}
