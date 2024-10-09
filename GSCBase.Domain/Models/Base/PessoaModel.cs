using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Models.Base
{
    public class PessoaModel : BaseViewModel
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string Sexo { get; set; }
        public DateTime? DtExpedicao { get; set; }
        public string RazaoSocial { get; set; }
        public string OrgaoExpeditor { get; set; }
        public string CpfCnpj { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public string InscEstadual { get; set; }
        public string Rg { get; set; }

        #region  Endereço 
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
        #endregion

        #region Contatos
        public string Telefone { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
        #endregion
        
    }
}
