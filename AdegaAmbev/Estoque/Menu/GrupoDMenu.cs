using AdegaAmbev.Estoque.Service;
using System;
using System.Threading;

namespace AdegaAmbev.Estoque.Menu
{
    public static class GrupoDMenu
    {
        public static void Iniciar()
        {
            Console.Title = "Módulo Time D";
            Console.Clear();
            Console.WriteLine("Seja bem vindo ao módulo do time D\n");

            Console.WriteLine("Digite a opção que você deseja\n");
            Console.WriteLine("1 - Módulo Estoque");
            Console.WriteLine("2 - Módulo Venda");
            Console.WriteLine("0 - Sair\n");
            Console.Write("Opção: ");


            switch (Console.ReadLine())
            {
                case "1":
                    EstoqueService.MenuEstoque();
                    break;

                case "2":
                    VendaService.MenuVenda();
                    break;

                case "0":
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Opção invalida, tente novamente...\n");
                    Thread.Sleep(2000);
                    Iniciar();
                    break;
            }
        }
    }
}
