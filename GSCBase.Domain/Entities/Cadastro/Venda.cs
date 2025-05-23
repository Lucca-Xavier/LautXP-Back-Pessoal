﻿using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCBase.Domain.Entities.Cadastro
{
    public class Venda : BaseModel
    {
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }



        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }

        public Venda() { }

        
        public Venda(int quantidade, int idCliente, int idProduto,  ApplicationUser user)
        {


            if ( quantidade < 1)
            {
                throw new ArgumentException($"'{nameof(quantidade)}' não pode ser nulo nem vazio.", nameof(quantidade));
            }

            Quantidade = quantidade;
            IdCliente = idCliente;
            IdProduto = idProduto;
            SetUserCreate(user);
        }

        // Este código altera algo
        public void Alterar(int quantidade, int idCliente, int idProduto, ApplicationUser user)
        {
            if (quantidade < 1)
            {
                throw new ArgumentException($"'{nameof(quantidade)}' não pode ser nulo nem vazio.", nameof(quantidade));
            }

            Quantidade = quantidade;
            IdCliente = idCliente;
            IdProduto = idProduto;
            SetUserCreate(user);
        }
        
        /*
        public Venda(int quantidade, Cliente cliente, Produto produto, ApplicationUser user)
        {

            // Validação de arquivo não nulo nem vazio

            if (quantidade < 1)
            {
                throw new ArgumentException($"'{nameof(quantidade)}' não pode ser nulo nem vazio.", nameof(quantidade));
            }

            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Produto = produto ?? throw new ArgumentNullException(nameof(produto));
            Quantidade = quantidade;
            SetUserCreate(user);
        }

        // Este código altera algo
        public void Alterar(int quantidade, Cliente cliente, Produto produto, ApplicationUser user)
        {
            if (quantidade < 1)
            {
                throw new ArgumentException($"'{nameof(quantidade)}' não pode ser nulo nem vazio.", nameof(quantidade));
            }

            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Produto = produto ?? throw new ArgumentNullException(nameof(produto));
            Quantidade = quantidade;
            SetUserAlter(user);
        }

        */
    }
}
