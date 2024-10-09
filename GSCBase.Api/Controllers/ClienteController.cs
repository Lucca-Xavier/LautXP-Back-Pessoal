using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Base;
using System.Collections.Generic;
using GSCBase.Domain.Models.Base;

namespace GSCBase.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClienteController : BaseController
    {
        private readonly IClienteService clienteService;
        private readonly IPessoaService pessoaService;
        private readonly IUnidadeService unidadeService;
        private readonly IModuleService moduleService;
        private readonly ICommonService commonService;
        public ClienteController(UserManager<ApplicationUser> userManager,
                                 IClienteService _clienteService,
                                 IPessoaService _pessoaService,
                                 IUnidadeService _unidadeService,
                                 IModuleService _moduleService,
                                 ICommonService _commonService) : base(userManager)
        {
            clienteService = _clienteService;
            pessoaService = _pessoaService;
            unidadeService = _unidadeService;
            moduleService = _moduleService;
            commonService = _commonService;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            var lstCliente = clienteService.Get().Take(100000);

            return Ok(lstCliente.Select(m => new ClienteModel
            {
                Id = m.Id,
                NmCliente = m.Pessoa.Nome,
                IsActive = m.IsActive,
            }));
        }

        [HttpGet]
        [Route("obter")]
        public IActionResult Obter(int id)
        {
            Cliente cliente = clienteService.FindById(id, c => c.Pessoa);

            if (cliente != null)
            {
                return Ok(new ClienteModel
                {
                    Id = cliente.Id,
                    NmCliente = cliente.Pessoa.Nome,
                    Bairro = cliente.Pessoa.Bairro,
                    Cidade = cliente.Pessoa.Cidade,
                    Estado = cliente.Pessoa.Estado,
                    Cep = cliente.Pessoa.Cep,
                    Logradouro = cliente.Pessoa.Logradouro,
                    Numero = cliente.Pessoa.Numero,
                    Complemento = cliente.Pessoa.Complemento,
                    Referencia = cliente.Pessoa.Referencia,
                    Telefone = cliente.Pessoa.Telefone,
                    TelefoneCelular = cliente.Pessoa.Celular,
                    Email = cliente.Pessoa.Email,
                    IsActive = cliente.IsActive,
                });
            }
            return BadRequest("Cliente não localizado.");
        }

        [HttpPost]
        [Route("salvar")]
        public IActionResult Salvar([FromBody] ClienteModel model)
        {
            ApplicationUser usuario = this.GetUsuarioLogado();
            Pessoa pessoa = pessoaService.FindById(model.Id);

            string estado = model.Estado;
            string cidade = model.Cidade;
            string bairro = model.Bairro;

            if (pessoa != null)
            {
                model.Id = pessoa.Id;
                pessoa.Alterar(model.Nome, model.RazaoSocial, model.CpfCnpj,
                                    model.Telefone, model.TelefoneCelular, model.Email, model.Cep, estado, cidade, bairro,
                                    model.Logradouro, model.Numero, model.Complemento, model.Referencia, model.Latitude, model.Longitude, model.Instagram, model.Facebook, model.Linkedin, usuario);
            }
            else
            {
                pessoa = new Pessoa(model.Nome, model.RazaoSocial, model.CpfCnpj,
                                    model.Telefone, model.TelefoneCelular, model.Email, model.Cep, estado, cidade, bairro, model.Logradouro,
                                    model.Numero, model.Complemento, model.Referencia, model.Latitude, model.Longitude, model.Instagram, model.Facebook, model.Linkedin, usuario);
            }

            Cliente cliente = clienteService.FindClienteCompleto(model.Id, this.GetIdUnidade(), this.GetIdModule());
            if (cliente == null)
            {
                Unidade unidade = unidadeService.FindById(this.GetIdUnidade());
                Module module = moduleService.FindById(this.GetIdModule());
                cliente = new Cliente(pessoa, null, null, this.GetUsuarioLogado());
            }

            clienteService.Save(cliente);

            return Ok();
        }

        [HttpGet]
        [Route("excluir")]
        public IActionResult Excluir(int id)
        {
            if (clienteService.Delete(id, this.GetUsuarioLogado()))
                return Ok();
            return BadRequest();
        }
    }
}