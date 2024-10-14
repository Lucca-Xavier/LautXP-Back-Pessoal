using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Publicidade : BaseModel
    {
        // Propriedades da classe

        public string Nome { get; set; }
        public string Arquivo { get; set; }

        public Publicidade(){}

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
            SetUserCreate(user);
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
            SetUserAlter(applicationUser);
        }
    }
}


