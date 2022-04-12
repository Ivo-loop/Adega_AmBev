using System;
using TestRoots;

namespace ClienteTestRoots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var tipoBebiba = new TipoBebidaServiceTeste();
            tipoBebiba.rodarTodosTestes();
        }

            var clienteTest = new ClienteTest();
            clienteTest.RodarTestesCliente();

            var valor = clienteService.ValidarCliente(cliente);

            if (valor != "sucesso")
            {
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
