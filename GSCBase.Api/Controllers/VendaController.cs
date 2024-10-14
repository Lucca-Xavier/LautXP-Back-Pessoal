using GSCBase.Application.IServices.Cadastro;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSCBase.Api.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class VendaController : BaseController
    {

        private readonly IVendaService service;
        private readonly IClienteService clienteService;
        private readonly IProdutoService produtoService;
        public VendaController(
           UserManager<ApplicationUser> userManager,
           IVendaService service,
           IClienteService clienteService,
           IProdutoService produtoService) : base(userManager)
        {
            this.service = service;
            this.clienteService = clienteService;
            this.produtoService = produtoService;
        }

        [HttpGet]
        public List<VendaModel> Get()
        {
            return service.GetAllVendas();

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var venda = service.GetVendaById(id);

            if (venda == null) return NotFound("Venda não encontrada");

            return Ok(venda);
        }

        [HttpPost]
        public IActionResult Post([FromBody] VendaModel model)
        {
            Venda venda;
            Cliente cliente = clienteService.FindById(model.IdCliente);
            Produto produto = produtoService.FindById(model.IdProduto);
            if (model.Id > 0)
            {
                venda = service.FindById(model.Id);
                venda.Alterar(model.Quantidade, cliente, produto, GetUsuarioLogado());
            }
            else
            {
                venda = new Venda(model.Quantidade, cliente, produto, GetUsuarioLogado());
            }
            service.Save(venda);
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
