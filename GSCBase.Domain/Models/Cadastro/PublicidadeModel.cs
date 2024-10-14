using Microsoft.AspNetCore.Http;

namespace GSCBase.Domain.Models.Cadastro
{
    public class PublicidadeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IFormFile Anexo { get; set; }
        public string Arquivo { get; set; }
    }
}
