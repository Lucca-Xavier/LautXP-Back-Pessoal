using GSCBase.Application.IServices.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Application.IServices.Cadastro
{
    public interface IVendaService : IBaseService<Venda>
    {

        List<VendaModel> GetAllVendas();
        VendaModel GetVendaById(int id);


        int CalcularPontos(int quantidade, int tamanho, int multiplicador);
    }
}
