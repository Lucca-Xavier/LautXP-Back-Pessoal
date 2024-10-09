using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Models.Base
{
    public class CidadeModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdEstado { get; set; }
        public EstadoModel Estado { get; set; }
    }


}
