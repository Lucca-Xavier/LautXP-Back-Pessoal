using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Produto : BaseModel
    {
        public string Rotulo { get; set; }
        public int Tamanho { get; set; }
        public int Multiplicador { get; set; }
        public Produto() { }
        public Produto(string rotulo, int tamanho, int multiplicador, ApplicationUser user)
        {
            if (string.IsNullOrEmpty(rotulo))
            {
                throw new System.ArgumentException($"'{nameof(rotulo)}' não pode ser nulo nem vazio.", nameof(rotulo));
            }

            Rotulo = rotulo;
            Tamanho = tamanho;
            Multiplicador = multiplicador;
            SetUserCreate(user);
        }

        public void Alterar(string rotulo, int tamanho, int multiplicador, ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(rotulo))
            {
                throw new System.ArgumentException($"'{nameof(rotulo)}' não pode ser nulo nem vazio.", nameof(rotulo));
            }

            Rotulo = rotulo;
            Tamanho = tamanho;
            Multiplicador = multiplicador;

            SetUserAlter(applicationUser);
        }
    }
}
