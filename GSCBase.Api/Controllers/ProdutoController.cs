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
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService service;
        public ProdutoController(
            UserManager<ApplicationUser> userManager,
            IProdutoService service) : base(userManager)
        {
            this.service = service;
        }

        [HttpGet]
        public List<ProdutoModel> Get()
        {
            return service.Get().Select(x => new ProdutoModel
            {
                Id = x.Id,
                Rotulo = x.Rotulo,
                Tamanho = x.Tamanho,
                IsActive = x.IsActive,                
            }).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produto = service.FindById(id);
            if (produto is null) return BadRequest("Não foi possível encontrar");

            return Ok(new ProdutoModel
            {
                Id = id,
                Rotulo = produto.Rotulo,
                Tamanho = produto.Tamanho,
                IsActive = produto.IsActive,
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProdutoModel model)
        {
            Produto produto;
            if (model.Id > 0)
            {
                produto = service.FindById(model.Id);
                produto.Alterar(model.Rotulo, model.Tamanho, GetUsuarioLogado());
            }
            else
            {
                produto = new Produto(model.Rotulo, model.Tamanho, GetUsuarioLogado());
            }
            service.Save(produto);
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
