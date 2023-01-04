using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco.Models
{
    public class Conta
    {
        public Conta(Pessoa cliente, string senha)
        {
            Cliente = cliente;
            Senha = senha;
            saldoConta = 0;
        }
        public Pessoa Cliente { get; set; }
        public string Senha { get; set; }
        private decimal saldoConta;

        public decimal Saldo
        {
            get { return saldoConta; }
        }

        public void RealizaDeposito(decimal valor)
        {
            saldoConta += valor;
        }

        public void RealizaSaque(decimal valor)
        {
            if (saldoConta >= valor)
            {
                saldoConta -= valor;
            }
            else
            {
                Console.WriteLine("Saldo insuficiente");
            }
        }

        public void RealizaTransferencia(Conta contaDestino, decimal valor)
        {
            if (this.saldoConta >= valor)
            {
                this.RealizaSaque(valor);
                contaDestino.RealizaDeposito(valor);
            }
            else
            {
                Console.WriteLine("Saldo insuficiente");
            }
        }

        public override string ToString()
        {
            return $"Nome: {Cliente.Nome}, CPF: {Cliente.Cpf}, Saldo: R${saldoConta:F2}";
        }

    }
}
