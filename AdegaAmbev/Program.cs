using AdegaAmbev.Clientes.Menu;
using System;
using AdegaAmbev.Estoque.Menu;
using AdegaAmbev.Produtos.Menu;
using AdegaAmbev.Comum;
using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;

namespace AdegaAmbev
{
    public class Program
    {
        static void Main(string[] args)
        {
            StartMenu();
        }

        public static void StartMenu()
        {
            var estoqueRepository = new EstoqueRepository();
            var vendaRepository = new VendaRepository();
            var estoqueService = new EstoqueService(estoqueRepository);
            var vendaService = new VendaService(estoqueRepository, vendaRepository);

            Console.Title = "Projeto Adega";
            CorConsole.Branco();
            Console.WriteLine("Escola um dos Menus Senhor Admin\n");
            while (true)
            {
                Console.WriteLine("1. Menu Cliente");
                Console.WriteLine("2. Menu Produto");
                Console.WriteLine("3. Menu Estoque");
                Console.WriteLine("0. Sair");
                Console.Write("Opção: ");
                switch (Console.Read())
                {
                    case '1':
                        Console.Title = "Grupo B";
                        Console.Clear();
                        Console.ReadLine();
                        MenuCliente.Menu();
                        break;
                    case '2':
                        Console.Title = "Grupo C";
                        Console.Clear();
                        Console.ReadLine();
                        MenuProduto.IniciarMenuProduto();
                        break;
                    case '3':
                        Console.Clear();
                        Console.ReadLine();
                        GrupoDMenu.Iniciar(estoqueService, vendaService, false);
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.ReadLine();
                        Console.WriteLine("Opção Invalida, Tente novamente.....");
                        break;
                }
            }
        }
    }
}
