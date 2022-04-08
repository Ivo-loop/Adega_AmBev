using AdegaAmbev.Clientes.Entidades;
using AdegaAmbev.Clientes.Service;
using AdegaAmbev.Comum;
using System;

namespace ClienteTestRoots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void testValidarDadosClientes()
        {
            ClienteService clienteService = new ClienteService();
            Cliente cliente = new Cliente()
            {
                Nome = "josue",
                Email = "josue.santos@gmail.com.br"
            };

            var valor = clienteService.ValidarCliente(cliente);

            if (valor != "sucesso") { 
                CorLetraConsole.Vermelho();
                Console.WriteLine("test de validar user falhando");
            }
            else
            {
                CorLetraConsole.Verde();
                Console.WriteLine("deu sucesso");
                Console.ResetColor();
            }

        }
    }
}
