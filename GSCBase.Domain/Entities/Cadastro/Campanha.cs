using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Campanha : BaseModel
    {
        public string Nome { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }


        public Campanha() { }

        public Campanha(string nome, DateTime inicio, DateTime fim, ApplicationUser user)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException($"'{nameof(nome)}' não pode ser nulo nem vazio.", nameof(nome));
            }

            Nome = nome;
            Inicio = inicio;
            Fim = fim;
            SetUserCreate(user);

        }
        public void Alterar(string nome, DateTime inicio, DateTime fim, ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException($"'{nameof(nome)}' não pode ser nulo nem vazio.", nameof(nome));
            }

            Nome = nome;
            Inicio = inicio;
            Fim = fim;
        }
    }
}
