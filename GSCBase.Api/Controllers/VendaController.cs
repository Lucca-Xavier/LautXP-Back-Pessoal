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


        [HttpGet("buscar")]
        public IActionResult BuscarClientes(string filtro)
        {
            var clientes = clienteService.Get(c => c.Nome.Contains(filtro) || c.Cpf.Contains(filtro) && c.IsActive)
                .Select(x => new ClienteModel
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Cpf = x.Cpf,
                    Pontos = x.Pontos,
                    Nascimento = x.Nascimento,
                }).ToList();

            return Ok(clientes);
        }



        [HttpPost]
        public IActionResult Post([FromBody] VendaModel model)
        {
            Venda venda;
            Produto produto = produtoService.FindById(model.IdProduto);
            Cliente cliente = clienteService.FindById(model.IdCliente);


            if (model.Id > 0)
            {
                venda = service.FindById(model.Id);
                venda.Alterar(model.Quantidade, model.IdCliente, model.IdProduto, GetUsuarioLogado());
            }
            else
            {

                venda = new Venda(model.Quantidade, model.IdCliente, model.IdProduto, GetUsuarioLogado());
            }

            int total = service.CalcularPontos(model.Quantidade, produto.Tamanho, produto.Multiplicador);
            cliente.Pontos += total;

            clienteService.Save(cliente);
            service.Save(venda);
            return Ok();
        }
        /*
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

               */

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (service.Delete(id, GetUsuarioLogado())) return Ok();

            return BadRequest("Não foi possível excluir o registro");
        }





    }
}
