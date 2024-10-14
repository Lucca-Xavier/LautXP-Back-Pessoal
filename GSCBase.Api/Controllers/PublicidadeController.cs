using GSCBase.Application.IServices.Cadastro;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GSCBase.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PublicidadeController : BaseController
    {
        private readonly IPublicidadeService service;
        private readonly IConfiguration _config;
        public PublicidadeController(
            UserManager<ApplicationUser> userManager,
            IPublicidadeService service,
            IConfiguration config) : base(userManager)
        {
            this.service = service;
            _config = config;
        }

        [HttpGet]
        public List<PublicidadeModel> Get()
        {
            return service.Get().Select(x => new PublicidadeModel
            {
                Id = x.Id,
                Nome = x.Nome
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
                Arquivo = publicidade.Arquivo
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PublicidadeModel model)
        {
            if (model.Anexo is null) return BadRequest("Anexos não selecionados");

            string ext = model.Anexo.FileName[model.Anexo.FileName.LastIndexOf(".")..];
            ext = ext.ToLower();

            if (ext != ".jpg" &&
                ext != ".jpeg" &&
                ext != ".png")
            {
                return BadRequest("Tipo de arquivo não suportado");
            }

            string diretorio = _config.GetValue<string>("Anexos");
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

            string nomeArquivo = Guid.NewGuid() + Path.GetExtension(model.Anexo.FileName);
            string path = Path.Combine(
                         _config.GetValue<string>("Anexos"), nomeArquivo);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await model.Anexo.CopyToAsync(stream);
            }

            Publicidade publicidade;
            if (model.Id > 0)
            {
                publicidade = service.FindById(model.Id);
                publicidade.Alterar(model.Nome, nomeArquivo, GetUsuarioLogado());
            }
            else
            {
                publicidade = new Publicidade(model.Nome, nomeArquivo, GetUsuarioLogado());
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






