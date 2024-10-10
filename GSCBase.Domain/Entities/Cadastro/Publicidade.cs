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
        public string Arquivo { get; set; }
        public string Formato { get; set; }


        // Construtor da classe
        public Publicidade(string arquivo, string formato)
        {

            // Validação de arquivo não nulo nem vazio

            if (string.IsNullOrEmpty(arquivo))
            {
                throw new System.ArgumentException($"'{nameof(arquivo)}' não pode ser nulo nem vazio.", nameof(arquivo));
            }

            if (string.IsNullOrEmpty(formato))
            {
                throw new System.ArgumentException($"'{nameof(formato)}' não pode ser nulo nem vazio.", nameof(formato));
            }

            Arquivo = arquivo;
            Formato = formato;
        }

         // Este código altera algo
        public void Alterar(string arquivo, string formato)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                throw new System.ArgumentException($"'{nameof(arquivo)}' não pode ser nulo nem vazio.", nameof(arquivo));
            }

            if (string.IsNullOrEmpty(formato))
            {
                throw new System.ArgumentException($"'{nameof(formato)}' não pode ser nulo nem vazio.", nameof(formato));
            }

            Arquivo = arquivo;
            Formato = formato;
        }
    }
}


