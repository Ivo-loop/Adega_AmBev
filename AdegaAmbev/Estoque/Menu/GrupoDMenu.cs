using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using System;
using System.Threading;

namespace AdegaAmbev.Estoque.Menu
{
    public static class GrupoDMenu
    {
        public static void Iniciar(EstoqueService estoqueService, VendaService vendaService, bool ehTeste)
        {
            Console.Title = "Módulo Time D";
            if(!ehTeste)
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
                    estoqueService.MenuEstoque();
                    break;

                case "2":
                    vendaService.MenuVenda();
                    break;

                case "0":
                    return;

                default:
                    if (!ehTeste)
                        Console.Clear();
                    Console.WriteLine("Opção invalida, tente novamente...\n");
                    Thread.Sleep(2000);
                    Iniciar(estoqueService, vendaService, ehTeste);
                    break;
            }
        }
    }
}
