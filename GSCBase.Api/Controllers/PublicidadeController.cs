using GSCBase.Application.IServices.Cadastro;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GSCBase.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PublicidadeController : BaseController
    {
        private readonly IPublicidadeService service;

        public PublicidadeController(
            UserManager<ApplicationUser> userManager,
            IPublicidadeService service) : base(userManager)
        {
            this.service = service;
        }

        [HttpGet]
        public List<PublicidadeModel> Get()
        {
            return service.Get(x => x.IsActive).Select(x => new PublicidadeModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Arquivo = x.Arquivo,
                
            }).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var publicidade = service.FindById(id);
            if (publicidade is null) return BadRequest("Não foi possível encontrar");

            return Ok(new PublicidadeModel
            {
                Id = id,
                Nome = publicidade.Nome,
                Arquivo = publicidade.Arquivo,
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PublicidadeModel model)
        {
            Publicidade publicidade;
            if (model.Id > 0)
            {
                publicidade = service.FindById(model.Id);
                publicidade.Alterar(model.Nome, model.Arquivo, GetUsuarioLogado());
            }
            else
            {
                publicidade = new Publicidade(model.Nome, model.Arquivo, GetUsuarioLogado());
            }
            service.Save(publicidade);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (service.Delete(id, GetUsuarioLogado())) return Ok();

            return BadRequest("Não foi possível excluir o registro");
        }
    }
}






