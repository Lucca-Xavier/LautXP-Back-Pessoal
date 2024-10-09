using GSCBase.Domain.Entities.Auth;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace GSCBase.Domain.Entities.Base
{
    public class Pessoa : BaseModel
    {
        public string Nome { get; set; }
        public string Rzsocial { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Referencia { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }

        public Pessoa() { }

        public Pessoa(string nome, string rzSocial, string cpfCnpj,
                       string telefone, string telefoneCelular, string email, string cep, string estado, string cidade, string bairro,
                       string logradoro, string numero, string complemento, string referencia, string latitude, string longitude,
                       string instagram, string facebook, string linkedin,
                       ApplicationUser user)
        {
            this.Nome = nome;
            this.Rzsocial = rzSocial;
            this.CpfCnpj = cpfCnpj;
            this.Telefone = telefone;
            this.Celular = telefoneCelular;
            this.Email = email;
            this.Cep = cep;
            this.Estado = estado;
            this.Cidade = cidade;
            this.Bairro = bairro;
            this.Logradouro = logradoro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Referencia = referencia;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Instagram = instagram;
            this.Facebook = facebook;
            this.Linkedin = linkedin;
            this.SetUserCreate(user);
        }

        public void Alterar(string nome, string rzSocial, string cpfCnpj,
                       string telefone, string telefoneCelular, string email, string cep, string estado, string cidade, string bairro,
                       string logradoro, string numero, string complemento, string referencia, string latitude, string longitude, string instagram, string facebook, string linkedin,
                      ApplicationUser user)
        {
            this.Nome = nome;
            this.Rzsocial = rzSocial;
            this.CpfCnpj = cpfCnpj;
            this.Telefone = telefone;
            this.Celular = telefoneCelular;
            this.Email = email;
            this.Cep = cep;
            this.Estado = estado;
            this.Cidade = cidade;
            this.Bairro = bairro;
            this.Logradouro = logradoro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Referencia = referencia;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Instagram = instagram;
            this.Facebook = facebook;
            this.Linkedin = linkedin;
            this.SetUserAlter(user);
        }

        public Pessoa AdicionarPessoaVenda(string nome, string telefone, string telefoneCelular, string email,ApplicationUser user)
        {
            Pessoa p = new Pessoa();
            p.Nome = nome;
            p.Telefone = telefone;
            p.Celular = telefoneCelular;
            p.Email = email;            
            p.SetUserCreate(user);
            return p;
        }
    }
}