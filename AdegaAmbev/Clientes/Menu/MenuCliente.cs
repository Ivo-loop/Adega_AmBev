using System;
using AdegaAmbev.Clientes.Service;
using AdegaAmbev.Clientes.Entidades;

namespace AdegaAmbev.Clientes.Menu
{
    public class MenuCliente
    {
        public async static void Menu() 
        {
            var service = new ClienteService();

            Console.WriteLine("MENU DE CLIENTES");
            Console.WriteLine("");
            Console.WriteLine("Escolha uma das opções...");
            Console.WriteLine("1. Cadastrar Cliente");
            Console.WriteLine("2. Atualizar Cliente");
            Console.WriteLine("3. Visualizar Cliente por Filtro");
            Console.WriteLine("4. Visualizar Todos os Clientes");
            Console.WriteLine("0. Sair");
            Console.Write("Opção: ");
            switch (Console.Read())
            {
                case '1':
                    Console.ReadLine();
                    var cliente = new Cliente();
                    Console.WriteLine("Insira o e-mail do cliente: ");
                    cliente.Email = Console.ReadLine();
                    Console.WriteLine("Insira o nome do cliente: ");
                    cliente.Nome = Console.ReadLine();
                    await service.CadastrarCliente(cliente);
                    break;
                case '2':
                    Console.ReadLine();
                    service.AtualizarCliente();
                    break;
                case '3':
                    Console.ReadLine();
                    Console.WriteLine("Selecione o tipo de filtro...");
                    Console.WriteLine("1. Filtro por Nome");
                    Console.WriteLine("2. Filtro por E-mail");
                    switch (Console.Read())
                    {
                        case '1':
                            Console.ReadLine();
                            Console.WriteLine("Insira o nome do cliente: ");
                            service.FiltrarClientePorNome(Console.ReadLine());
                            break;
                        case '2':
                            Console.ReadLine();
                            Console.WriteLine("Insira o e-mail do cliente: ");
                            //service.FiltrarClientePorEmail(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("Opção inválida, tente novamente...");
                            //todo: chamar o case 3 de novo
                            break;
                    }
                    break;
                case '4':
                    break;
                case '0':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente...");
                    Menu();
                    break;
            }
        }
    }
}