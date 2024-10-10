using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Produto : BaseModel
    {
        public string Rotulo { get; set; }
        public int Tamanho { get; set; }
        public Produto() { }
        public Produto(string rotulo, int tamanho, ApplicationUser user)
        {
            if (string.IsNullOrEmpty(rotulo))
            {
                throw new System.ArgumentException($"'{nameof(rotulo)}' não pode ser nulo nem vazio.", nameof(rotulo));
            }

            Rotulo = rotulo;
            Tamanho = tamanho;
            SetUserCreate(user);
        }

        public void Alterar(string rotulo, int tamanho, ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(rotulo))
            {
                throw new System.ArgumentException($"'{nameof(rotulo)}' não pode ser nulo nem vazio.", nameof(rotulo));
            }

            Rotulo = rotulo;
            Tamanho = tamanho;
            SetUserAlter(applicationUser);
        }
    }
}
