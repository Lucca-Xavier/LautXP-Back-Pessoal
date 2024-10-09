using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Entities.Base
{
    public class Bairro
    {
        public int Id{ get; set; }
        public string Nome { get; set; }
        public int IdCidade { get; set; }        
        public Cidade Cidade { get; set; }
    }
}
