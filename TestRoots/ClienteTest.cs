using AdegaAmbev.Clientes.Entidades;
using AdegaAmbev.Clientes.Service;
using AdegaAmbev.Comum;
using System;
using System.Linq;
using TestRoots.Common;

namespace TestRoots
{
    internal class ClienteTest
    {
        ClienteService _clienteService = new ClienteService();
        public void Deve_Validar_Os_Dados_Do_Cliente()
        {
            Console.WriteLine("\nTeste validar cliente:");
            limpar_banco();

            Cliente cliente = new Cliente()
            {
                Nome = "josue",
                Email = "josue.santos@gmail.com.br"
            };

            var valor = _clienteService.ValidarCliente(cliente);

            if (valor != "sucesso")
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Falha em algum dado do usuário");
            }

            else
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Cliente validado com sucesso");
            }
        }

        public void Deve_Cadastrar_O_Cliente()
        {
            Console.WriteLine("\nTeste cadastrar cliente:");
            limpar_banco();

            Cliente cliente = new Cliente()
            {
                Nome = "josue",
                Email = "josue.santos@gmail.com.br"
            };

            _clienteService.CadastrarCliente(cliente);
            var clienteCadastrado = _clienteService.FiltrarClientePorNome(cliente.Nome);

            if (clienteCadastrado.Nome == cliente.Nome)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Cliente cadastrado com sucesso!");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Cliente não cadastrado!");
            }
        }

        public void Deve_Obter_Todos_Os_Clientes()
        {
            Console.WriteLine("\nTeste obter todos os clientes:");
            limpar_banco();

            Cliente cliente1 = new Cliente()
            {
                Nome = "josue",
                Email = "josue.santos@gmail.com.br"
            };

            Cliente cliente2 = new Cliente()
            {
                Nome = "mario",
                Email = "mario.santos@gmail.com.br"
            };

            _clienteService.CadastrarCliente(cliente1);
            _clienteService.CadastrarCliente(cliente2);

            var clientes = _clienteService.ObterTodosClientes();

            if (clientes.Count() > 0)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Clientes obtidos com sucesso!");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Cliente não obtidos!");
            }
        }

        public void Deve_Filtar_Cliente_Por_Nome()
        {
            Console.WriteLine("\nTeste filtrar cliente por email:");
            limpar_banco();

            Cliente cliente = new Cliente()
            {
                Nome = "josue",
                Email = "josue.santos@gmail.com.br"
            };

            _clienteService.CadastrarCliente(cliente);
            var clienteCadastrado = _clienteService.FiltrarClientePorNome(cliente.Nome);

            if (clienteCadastrado.Nome == cliente.Nome)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Cliente filtrado por nome com sucesso!");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Cliente não filtrado por nome!");
            }
        }

        public void Deve_Filtar_Cliente_Por_Email()
        {
            Console.WriteLine("\nTeste filtrar cliente por email:");
            limpar_banco();

            Cliente cliente = new Cliente()
            {
                Nome = "josue",
                Email = "josue.santos@gmail.com.br"
            };

            _clienteService.CadastrarCliente(cliente);
            var clienteCadastrado = _clienteService.FiltrarClientePorEmail(cliente.Email);

            if (clienteCadastrado.Email == cliente.Email)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Cliente filtrado por email com sucesso!");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Cliente não filtrado por email!");
            }
        }

        public void RodarTestesCliente()
        {
            Deve_Validar_Os_Dados_Do_Cliente();
            Deve_Cadastrar_O_Cliente();
            Deve_Obter_Todos_Os_Clientes();
            Deve_Filtar_Cliente_Por_Nome();
            Deve_Filtar_Cliente_Por_Email();
        }

        private void limpar_banco()
        {
            new BancoUtils("Cliente.json");
        }
    }
}
