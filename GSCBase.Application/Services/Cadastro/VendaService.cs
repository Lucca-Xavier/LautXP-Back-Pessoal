using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Application.Services.Cadastro
{
    public class VendaService : BaseService<Venda>
    {
        private readonly IVendaRepository vendaRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IProdutoRepository produtoRepository;


        public VendaService(IVendaRepository repository) : base(repository)
        {
            this.vendaRepository = repository;
        }

        public List<VendaModel> GetAllVendas()
        {
            return vendaRepository.Get()
                .Select(v => new VendaModel
                {
                    Id = v.Id,
                    IdCliente = v.IdCliente,
                    IdProduto = v.IdProduto,
                    Quantidade = v.Quantidade,
                    NomeCliente = v.Cliente.Nome,
                    RotuloProduto = v.Produto.Rotulo
                }).ToList();
        }

        public VendaModel GetVendaById(int id)
        {
            var venda = vendaRepository.Get()
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .FirstOrDefault(v => v.Id == id);

            if (venda == null) return null;

            return new VendaModel
            {
                Id = venda.Id,
                IdCliente = venda.IdCliente,
                IdProduto = venda.IdProduto,
                Quantidade = venda.Quantidade,
                NomeCliente = venda.Cliente.Nome,
                RotuloProduto = venda.Produto.Rotulo
            };
        }


        public void Save(VendaModel model)
        {
            var cliente = clienteRepository.FindById(model.IdCliente);
            var produto = produtoRepository.FindById(model.IdProduto);

            var venda = new Venda
            {
                IdCliente = model.IdCliente,
                IdProduto = model.IdProduto,
                Quantidade = model.Quantidade,
                Cliente = cliente,
                Produto = produto
            };
            vendaRepository.Save(venda);
        }



    }
}
