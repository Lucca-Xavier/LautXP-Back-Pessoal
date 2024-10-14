using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Publicidade : BaseModel
    {
        // Propriedades da classe

        public string Nome { get; set; }
        public string Arquivo { get; set; }


        // Construtor da classe
        public Publicidade(string nome, string arquivo, ApplicationUser user)
        {

            // Validação de arquivo não nulo nem vazio

            if (string.IsNullOrEmpty(arquivo))
            {
                throw new System.ArgumentException($"'{nameof(arquivo)}' não pode ser nulo nem vazio.", nameof(arquivo));
            }

            if (string.IsNullOrEmpty(nome))
            {
                throw new System.ArgumentException($"'{nameof(nome)}' não pode ser nulo nem vazio.", nameof(nome));
            }

            Arquivo = arquivo;
            Nome = nome;
        }

         // Este código altera algo
        public void Alterar(string nome, string arquivo, ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                throw new System.ArgumentException($"'{nameof(arquivo)}' não pode ser nulo nem vazio.", nameof(arquivo));
            }

            if (string.IsNullOrEmpty(nome))
            {
                throw new System.ArgumentException($"'{nameof(nome)}' não pode ser nulo nem vazio.", nameof(nome));
            }

            Arquivo = arquivo;
            Nome = nome;
        }
    }
}


