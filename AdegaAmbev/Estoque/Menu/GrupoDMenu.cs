﻿using AdegaAmbev.Estoque.Service;
using System;
using System.Threading;

namespace AdegaAmbev.Estoque.Menu
{
    public static class GrupoDMenu
    {
        public static void Iniciar()
        {
            Console.Clear();
            Console.WriteLine("Seja bem vindo ao módulo do time D\n");

            Console.WriteLine("Digite a opção que você deseja\n");

            Console.WriteLine("1 - Inserir Estoque");
            //Console.WriteLine("2 - Inserir Tipo Venda");
            Console.WriteLine("2 - Realizar uma Venda");
            Console.WriteLine("0 - Sair\n");
            Console.Write("Opção: ");


            switch (Console.ReadLine())
            {
                case "1":
                    EstoqueService.InserirEstoque();
                    break;

                case "2":
                    VendaService.RealizarVenda();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção invalida, tente novamente...\n");
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
            }

            Iniciar(); // Depois é preicso alterar esse cara para integrar com os menus de outras equipes.

        }
    }
}
