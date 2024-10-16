using System;

namespace GSCBase.Domain.Models.Cadastro
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Pontos { get; set; }
        public DateTime Nascimento { get; set; }
    }
}
