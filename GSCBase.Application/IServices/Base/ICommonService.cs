using GSCBase.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Application.IServices.Base
{
    public interface ICommonService
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
