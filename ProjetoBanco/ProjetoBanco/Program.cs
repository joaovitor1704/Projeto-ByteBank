using ProjetoBanco.Models;

namespace ProjetoBanco
{
    class Program
    {
        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("0 - Caso deseja sair do programa");
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar usuário");
            Console.WriteLine("3 - Listar contas registradas");
            Console.WriteLine("4 - Dados de um usuário");
            Console.WriteLine("5 - Fazer login");
            Console.WriteLine("Digite a opção desejada: ");
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            Banco banco = new();
            int opcao;
            do
            {
                Menu();
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        banco.CriaConta();
                        Console.WriteLine("Usuario criado!");
                        break;
                    case 2:
                        banco.DeletarUsuario();
                        break;
                    case 3:
                        banco.ListaContas();
                        break;
                    case 4:
                        banco.DadosConta();
                        break;
                    case 5:
                        banco.RealizaLogin();
                        break;
                }
            }
            while (opcao != 0);

        }

    }

}

