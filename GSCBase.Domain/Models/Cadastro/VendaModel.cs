using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Domain.Models.Cadastro
{
    public class VendaModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public string RotuloProduto { get; set; }
        public string NomeCliente { get; set; }


    }
}
