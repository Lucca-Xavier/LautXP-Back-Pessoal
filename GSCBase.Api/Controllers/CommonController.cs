using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace GSCBase.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]

    public class CommonController : BaseController
    {
        private readonly ICommonService commonService;

        public CommonController(UserManager<ApplicationUser> userManager,
                                 ICommonService _commonService
                                 ) : base(userManager)
        {
            commonService = _commonService;
        }

        [HttpGet]
        [Route("listarEstado")]
        public IActionResult ListarEstado()
        {
            ICollection<Estado> lstEstado = commonService.ListarEstados();

            return Ok(lstEstado.Select(p => new EstadoModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Sigla = p.Sigla
            }));
        }

        [HttpGet]
        [Route("listarCidadesPorEstado")]
        public IActionResult ListarCidadesPorEstado(int idEstado)
        {
            ICollection<Cidade> lstCidade = commonService.ListarCidades(idEstado);

            return Ok(lstCidade.Select(p => new CidadeModel
            {
                Id = p.Id,
                Nome = p.Nome
            }));
        }

        [HttpGet]
        [Route("listarBairrosPorCidade")]

        public IActionResult ListarBairrosPorCidade(int idCidade)
        {
            ICollection<Bairro> lstBairro = commonService.ListarBairros(idCidade);

            return Ok(lstBairro.Select(p => new CidadeModel
            {
                Id = p.Id,
                Nome = p.Nome
            }));

        }

    }
}
