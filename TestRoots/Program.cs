using AdegaAmbev.Clientes.Entidades;
using AdegaAmbev.Clientes.Service;
using AdegaAmbev.Comum;
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
        }
    }
}
