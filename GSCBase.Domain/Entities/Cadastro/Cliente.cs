using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Models.Auth;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Cliente : BaseModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Cliente() {}
        public Cliente(string nome, string cpf, Auth.ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(nome)) 
            {
                throw new System.ArgumentException($"'{nameof(nome)}'não pode ser vazio.", nameof(nome));
            }

            if (string.IsNullOrEmpty(cpf))
            {
                throw new System.ArgumentException($"'{nameof(cpf)}'não pode ser vazio.", nameof(cpf));
            }
            else 
            {
                Valida.CpfCnpj(cpf);
            }

            Nome = nome;
            Cpf = cpf;
            SetUserCreate(applicationUser);
        }

        public void Alterar(string nome, string cpf, Auth.ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new System.ArgumentException($"'{nameof(nome)}'não pode ser vazio.", nameof(nome));
            }

            if (string.IsNullOrEmpty(cpf))
            {
                throw new System.ArgumentException($"'{nameof(cpf)}'não pode ser vazio.", nameof(cpf));
            }
            else
            {
                Valida.CpfCnpj(cpf);
            }

            Nome = nome;
            Cpf = cpf;
            SetUserAlter(applicationUser);
        }
    }
}
