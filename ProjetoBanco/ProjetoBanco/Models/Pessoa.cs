using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco.Models
{
    public class Pessoa
    {
        public Pessoa(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }

    }
}
