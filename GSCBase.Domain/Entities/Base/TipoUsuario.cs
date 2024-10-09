using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Entities.Base
{
    public class TipoUsuario : BaseModel
    {
        public string Nome { get; set; }

        public TipoUsuario() { }

        public TipoUsuario(string nome)
        {
            this.Nome = Valida.CampoNulo("Nome", nome);
        }
    }   
}
