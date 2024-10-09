using System;
using System.Collections.Generic;
using System.Text;

namespace GSCBase.Domain.Models.Base
{
    public class BairroModel
    {
        public int IdBairro { get; set; }
        public string Nome { get; set; }
        public int IdCidade { get; set; }
        public CidadeModel Cidade { get; set; }
    }
}
