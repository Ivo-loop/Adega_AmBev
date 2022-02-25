using AdegaAmbev.Clientes.Entidades;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdegaAmbev.Clientes.Service
{
    public class ClienteService
    {
        public string ValidarCliente(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nome))
                return "Nome do cliente não informado";

            if (string.IsNullOrEmpty(cliente.Email))
                return "E-mail do cliente não informado";

            if (!(cliente.Email.IndexOf('@') > 0))
                return "E-mail inválido";

            return "sucesso";
        }

        public void CadastrarCliente(Cliente cliente)
        {
            string fileName = "Banco/Cliente.json";

            var clientes = ObterTodosClientes();

            if (clientes.Any())
            {
                var validacao = ValidarCliente(cliente);
                if (validacao != "sucesso")
                    return;
            }

            if (clientes.Any(x => x.Email == cliente.Email))
            {
                Console.WriteLine("Cliente já cadastrado");
            }
            else
            {
                var qtdClientes = clientes.Count();
                cliente.Id = ++qtdClientes;
                clientes.Add(cliente);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(fileName, json);
                Console.WriteLine("Cliente cadastrado com sucesso");
            }
        }

        public List<Cliente> ObterTodosClientes()
        {
            using (StreamReader r = new StreamReader("Banco/Cliente.json"))
            {
                List<Cliente> clientes = new List<Cliente>();

                string json = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                {
                    return clientes;
                }

                clientes = JsonSerializer.Deserialize<List<Cliente>>(json);
                return clientes;
            }
        }

        public Cliente FiltrarClientePorNome(string nome)
        {
            var clientes = ObterTodosClientes();
            var cliente = clientes.Where(x => x.Nome == nome);
            return cliente.FirstOrDefault();
        }

        public Cliente FiltrarClientePorEmail(string email)
        {
            var clientes = ObterTodosClientes();
            var cliente = clientes.Where(x => x.Email == email);
            return cliente.FirstOrDefault();
        }

        public void AtualizarCliente()
        {
            var clientes = ObterTodosClientes();
            Console.WriteLine("Digite o e-mail do cliente");

            var emailClienteBusca = Console.ReadLine();

            if (!clientes.Any(x => x.Email == emailClienteBusca))
            {
                Console.WriteLine("E-mail não encontrado");
                return;
            }

            Console.WriteLine("Digite o novo nome do Cliente: ");
            var nomeCliente = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Digite o novo email do Cliente: ");
            var emailCliente = Convert.ToString(Console.ReadLine());

            foreach (var cliente in clientes.Where(obj => obj.Email == emailClienteBusca))
            {
                cliente.Nome = !string.IsNullOrEmpty(nomeCliente) ? nomeCliente : "";
                cliente.Email = !string.IsNullOrEmpty(emailCliente) ? emailCliente : "";
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("Banco/Cliente.json", json);
            Console.WriteLine("Registro atualizado com sucesso");
        }

    }
}