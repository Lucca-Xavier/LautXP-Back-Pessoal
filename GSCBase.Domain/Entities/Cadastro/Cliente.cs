using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Models.Auth;
using System;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Cliente : BaseModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Pontos { get; set; }
        public DateTime Nascimento { get; set; }
        public Cliente() {}
        public Cliente(string nome, string cpf, int pontos, DateTime nascimento, ApplicationUser applicationUser)
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
            Pontos = pontos;
            Nascimento = nascimento;
            SetUserCreate(applicationUser);
        }

        public void Alterar(string nome, string cpf, int pontos, DateTime nascimento, ApplicationUser applicationUser)
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
            Pontos = pontos;
            Nascimento = nascimento;
            SetUserAlter(applicationUser);
        }
    }
}
