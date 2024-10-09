using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.IRepositories;
using GSCBase.Infrastructure.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Application.Services.Base
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository repository;
        public CommonService(ICommonRepository _repository) 
        {
            repository = _repository;
        }

        public List<Bairro> ListarBairros()
        {
             return repository.ListarBairros();
        }

        public List<Bairro> ListarBairros(int? idCidade)
        {
            return repository.ListarBairros(idCidade);
        }

        public List<Cidade> ListarCidades()
        {
            return repository.ListarCidades();
        }

        public List<Cidade> ListarCidades(int? idEstado)
        {
            return repository.ListarCidades(idEstado);
        }

        public List<Estado> ListarEstados()
        {
            return repository.ListarEstados();
        }

        public Bairro ObterBairroPorId(int? idBairro)
        {
            return repository.ObterBairroPorId(idBairro);
        }

        public Cidade ObterCidadePorId(int? idCidade)
        {
            return repository.ObterCidadePorId(idCidade);
        }

        public Estado ObterEstadoPorId(int? idEstado)
        {
            return repository.ObterEstadoPorId(idEstado);
        }
    }
}
