using System;
using TestRoots;

namespace ClienteTestRoots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var clienteTest = new ClienteTest();
            clienteTest.RodarTestesCliente();

            var estoqueTest = new EstoqueTest();
            estoqueTest.Deve_atualizar_quantidade_em_estoque();
            estoqueTest.Deve_subtrair_quantidade_em_estoque();
        }
    }
}
