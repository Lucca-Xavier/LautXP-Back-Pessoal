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
    public class ClienteController : BaseController
    {
        private readonly IClienteService service;
        public ClienteController(
            UserManager<ApplicationUser> userManager,
            IClienteService service) : base(userManager)
        {
            this.service = service;
        }

        [HttpGet]
        public List<ClienteModel> Get()
        {
            return service.Get(x => x.IsActive).Select(x => new ClienteModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Cpf = x.Cpf
            }).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cliente = service.FindById(id);
            if (cliente is null) return BadRequest("Não foi possível encontrar");

            return Ok(new ClienteModel
            {
                Id = id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteModel model)
        {
            Cliente cliente;
            if (model.Id > 0)
            {
                cliente = service.FindById(model.Id);
                cliente.Alterar(model.Nome, model.Cpf, GetUsuarioLogado());
            }
            else
            {
                cliente = new Cliente(model.Nome, model.Cpf, GetUsuarioLogado());
            }
            service.Save(cliente);
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
