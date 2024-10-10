using GSCBase.Application.IServices.Cadastro;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
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
        private object formato;

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
                Arquivo = x.Arquivo,
                Formato = x.Formato
            }).ToList();
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            var arquivo = service.FindById(id);
            if (formato is null) return BadRequest("Não foi possível encontrar");

            return Ok(new PublicidadeModel { Id = id, Arquivo = arquivo.Arquivo, Formato = formato.Formato });


        }
    
    

    }

    public class PublicidadeModel
    {
        public string Formato { get; internal set; }
        public string Arquivo { get; internal set; }
        public int Id { get; internal set; }
    }
}





