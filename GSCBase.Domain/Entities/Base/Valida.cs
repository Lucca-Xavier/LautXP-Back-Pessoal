using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GSCBase.Domain.Entities.Base
{
    public static class Valida
    {
        public static string CampoNulo(string nome, string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new Exception("O campo \"" + nome + "\" é obrigatório");
            return valor;
        }

        public static T EntidadeNull<T>(string nome, T obj)
        {
            if (obj != null)
                return obj;
            else
                throw new Exception(nome + " é obrigatório.");
        }
        
        public static T EntidadeNull<T>(T obj)
        {
            if (obj != null)
                return obj;
            else
                throw new Exception(obj.GetType() + " é obrigatório.");
        }

        public static DateTime Data(DateTime? Data)
        {
            if (Data == null)
                throw new Exception("Data Inválida");
            return Data.GetValueOrDefault();
        }

        public static string CpfCnpj(string cpfcnpj)
        {
            Regex rg = new Regex(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})");
            if (string.IsNullOrEmpty(cpfcnpj) || !rg.IsMatch(cpfcnpj))
                throw new Exception("CPF/CPNJ inválido!");
            return SomenteNumeros(cpfcnpj);
        }

        public static string SomenteNumeros(string valor)
        {
            return String.Join("", Regex.Split(valor, @"[^\d]"));
        }

        public static int NumeroPositivo(string nome, int valor)
        {
            if (valor < 0)
                throw new Exception("O campo \"" + nome + "\" deve ser maior que 0");
            
            return valor;
        }
        
        public static decimal NumeroPositivo(string nome, decimal valor)
        {
            if (valor < 0)
                throw new Exception("O campo \"" + nome + "\" deve ser maior que 0");
            
            return valor;
        }
    }
}
