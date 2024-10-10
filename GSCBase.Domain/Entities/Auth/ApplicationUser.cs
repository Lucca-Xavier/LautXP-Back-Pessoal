using Microsoft.AspNetCore.Identity;
using GSCBase.Domain.Entities.Base;
using System;

namespace GSCBase.Domain.Entities.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime DtCreate { get; set; }
        public string UserCreate { get; set; }
        public DateTime? DtAlter { get; set; }
        public string UserAlter { get; set; }
        public DateTime? DtDeleted { get; set; }
        public string UserDeleted { get; set; }
        public long? FacebookId { get; set; }
        public string ResetPasswordCode { get; set; }
        public string Nome { get; set; }
        public int? IdPessoa { get; set; }
        public Pessoa Pessoa { get; set; }
        public int? IdTipoUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }


        public ApplicationUser()
        {

        }
        public ApplicationUser(string userName, string phoneNumber, string email, string userCreate, string nome)
        {
            this.UserName = userName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Nome = nome;
            this.IsActive = true;
            this.DtCreate = DateTime.Now;
            this.UserCreate = userCreate;
            IdTipoUsuario = 1;
        }

        public void DefinirTipoUsuario(TipoUsuario tipoUsuario, string userAlter)
        {
            this.TipoUsuario = tipoUsuario;
            this.IsActive = true;
            this.DtCreate = DateTime.Now;
            this.UserCreate = userAlter;
        }

        public void Alterar(string email, string telefoneCelular,string nome, string id)
        {
            this.Email = email;
            this.Nome = nome;
            this.PhoneNumber = telefoneCelular;
            this.UserAlter = id;
            this.DtAlter = DateTime.Now;
        }
    }
}
