using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBanco.Models
{
    public class Banco
    {
        public Banco()
        {
            ContasBanco = new List<Conta>();
        }
        public List<Conta> ContasBanco { get; set; }
        public bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public void DigitaDadosConta()
        {
            Console.WriteLine("Digite seu CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("Digite sua senha: ");
            string senha = Console.ReadLine();
        }

        public void RealizaLogin()
        {
            bool login = false;
            int tentativas = 0;
            while (!login)
            {
                if (tentativas > 2)
                {
                    Console.WriteLine("Muitas tentativas incorretas. Tente novamente mais tarde.");
                    break;
                }
                Console.WriteLine("Digite seu CPF: ");
                string cpf = Console.ReadLine();
                Console.WriteLine("Digite sua senha: ");
                string senha = Console.ReadLine();
                if (!ContasBanco.Exists(x => x.Cliente.Cpf == cpf))
                {
                    tentativas++;
                    Console.WriteLine("Não existe nenhuma conta com esse CPF. ");
                    Console.WriteLine("Digite os dados da conta novamente.");
                }
                else
                {
                    var conta = ContasBanco.Find(x => x.Cliente.Cpf == cpf);
                    if (conta.Senha != senha)
                    {
                        tentativas++;
                        Console.WriteLine("Senha incorreta");
                        Console.WriteLine("Digite os dados da conta novamente.");
                    }
                    else
                    {
                        login = true;
                        Console.WriteLine("Login realizado com sucesso\n");
                        MenuOperacoes(conta);

                    }
                }
            }
        }

        public void CriaConta()
        {
            Console.WriteLine("Digite seu CPF: ");
            string cpf = Console.ReadLine();
            while (!IsCpf(cpf))
            {
                Console.WriteLine("CPF inválido, digite novamente");
                cpf = Console.ReadLine();
            }
            while (ContasBanco.Exists(x => x.Cliente.Cpf == cpf))
            {
                Console.WriteLine("CPF já existente, digite novamente");
                cpf = Console.ReadLine();

            }
            Console.WriteLine("Digite o seu nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite a sua senha: ");
            string senha = Console.ReadLine();
            Pessoa pessoa = new Pessoa(nome, cpf);
            Conta conta = new Conta(pessoa, senha);
            ContasBanco.Add(conta);
        }

        public void ListaContas()
        {
            if (ContasBanco.Count == 0)
            {
                Console.WriteLine("Nenhuma conta criada");
            }
            else
            {
                foreach (var item in ContasBanco)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        public void Depositar(Conta conta)
        {
            Console.WriteLine("Digite o valor que você deseja depositar: ");
            decimal valor = decimal.Parse(Console.ReadLine());
            conta.RealizaDeposito(valor);
        }

        public void Sacar(Conta conta)
        {
            Console.WriteLine("Digite o valor que você deseja sacar: ");
            decimal valor = decimal.Parse(Console.ReadLine());
            conta.RealizaSaque(valor);
        }

        public void Transferir(Conta conta)
        {
            Console.WriteLine("Digite o CPF da conta que deseja transferir");
            string cpf = Console.ReadLine();
            if (!ContasBanco.Exists(x => x.Cliente.Cpf == cpf))
            {
                Console.WriteLine("Conta inválida.");
            }
            else
            {
                Console.WriteLine("Digite o valor que você deseja transferir: ");
                decimal valor = decimal.Parse(Console.ReadLine());
                var contaDestino = ContasBanco.Find(x => x.Cliente.Cpf == cpf);
                conta.RealizaTransferencia(contaDestino, valor);
            }
        }

        public void MenuOperacoes(Conta conta)
        {

            int opcao;
            do
            {
                Console.WriteLine();
                Console.WriteLine("0 - Fazer logout");
                Console.WriteLine("1 - Realizar saque");
                Console.WriteLine("2 - Realizar depósito");
                Console.WriteLine("3 - Realizar transferência");
                Console.WriteLine("Digite a opção desejada: ");
                Console.WriteLine();
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Sacar(conta);
                        break;
                    case 2:
                        Depositar(conta);
                        break;
                    case 3:
                        Transferir(conta);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
            while (opcao != 0);
        }

        public void DadosConta()
        {
            Console.WriteLine("Digite o CPF da conta que deseja saber os dados: ");
            string cpf = Console.ReadLine();
            if (!ContasBanco.Exists(x => x.Cliente.Cpf == cpf))
            {
                Console.WriteLine("Conta inválida.");
            }
            else
            {
                Console.WriteLine(ContasBanco.Find(x => x.Cliente.Cpf == cpf).ToString());
            }
        }

        public void DeletarUsuario()
        {
            Console.WriteLine("Digite o CPF da conta que deseja deletar: ");
            string cpf = Console.ReadLine();
            if (!ContasBanco.Exists(x => x.Cliente.Cpf == cpf))
            {
                Console.WriteLine("Conta inválida.");
            }
            else
            {
                ContasBanco.Remove(ContasBanco.Find(x => x.Cliente.Cpf == cpf));
            }
        }
    }
}
