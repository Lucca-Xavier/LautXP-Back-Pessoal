using GSCBase.Application.IServices.Cadastro;
using GSCBase.Application.Services.Base;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Domain.Models.Cadastro;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Application.Services.Cadastro
{
    public class VendaService : BaseService<Venda>, IVendaService
    {
        private readonly IVendaRepository vendaRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IProdutoRepository produtoRepository;


        public VendaService(
          IVendaRepository vendaRepository,
          IClienteRepository clienteRepository,
          IProdutoRepository produtoRepository) : base(vendaRepository)
        {
            this.vendaRepository = vendaRepository;
            this.clienteRepository = clienteRepository;
            this.produtoRepository = produtoRepository;

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


        
        public int CalcularPontos(int quantidade, int tamanho, int multiplicador)
        {
            return quantidade * tamanho * multiplicador;
        }
        


    }
}
