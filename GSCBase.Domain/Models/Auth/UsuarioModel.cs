using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Models.Auth
{
    public class UsuarioModel
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TelefoneCelular { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}
