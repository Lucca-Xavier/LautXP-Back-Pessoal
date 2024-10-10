using GSCBase.Application.IServices.Cadastro;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GSCBase.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    public class CampanhaController : BaseController
    {

        private readonly ICampanhaService service;

        public CampanhaController(
            UserManager<ApplicationUser> userManager,
            ICampanhaService service) : base(userManager)
        {
            this.service = service;
        }

        [HttpGet]
        public List<CampanhaModel> Get()
        {
            return service.Get().Select(x => new CampanhaModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Inicio = x.Inicio,
                Fim = x.Fim,
                IsActive = x.IsActive,

            }).ToList();
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int id) {
        
            var campanha = service.FindById(id);
            if (campanha == null) return BadRequest("Não foi possivel encontrar");


            return Ok(new CampanhaModel
            {
                Id = id,
                Nome = campanha.Nome,
                Inicio = campanha.Inicio,
                Fim = campanha.Fim,
                IsActive = campanha.IsActive,
            });

        }


        [HttpPost]
        public IActionResult Post([FromBody] CampanhaModel model)
        {
            Campanha campanha;
            if (model.Id > 0)
            {
                campanha = service.FindById(model.Id);
                campanha.Alterar(model.Nome, model.Inicio, model.Fim, GetUsuarioLogado());
            }
            else
            {
                campanha = new Campanha(model.Nome, model.Inicio, model.Fim, GetUsuarioLogado());
            }
            service.Save(campanha);
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
