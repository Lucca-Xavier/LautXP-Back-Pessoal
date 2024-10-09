using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Entities.Base
{
    public class Cidade
    {
        public int Id{ get; set; }
        public string Nome { get; set; }               
        public int IdEstado { get; set; }
        public Estado Estado { get; set; }
    }
}
