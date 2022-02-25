using AdegaAmbev.Clientes.Entidades;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AdegaAmbev.Clientes.Service
{
    public class ClienteService
    {
        public string ValidarCliente(Cliente cliente){
            if (string.IsNullOrEmpty(cliente.Nome))
                return "Nome do cliente não informado";
            
            if (string.IsNullOrEmpty(cliente.Email))
                return "E-mail do cliente não informado";
            
            if (!(cliente.Email.IndexOf('@') > 0))
                return "E-mail inválido";
            
            return "sucesso";
        }

        public async Task CadastrarCliente(Cliente cliente){
            string fileName = "Banco/Cliente.json";

            var clientes = ObterTodosClientes();

            if (clientes.Any())
            {
                var validacao = ValidarCliente(cliente);
                if (validacao != "sucesso")
                    return;
            }

            if (clientes.Any(x => x.Email == cliente.Email)){
                Console.WriteLine("Cliente já cadastrado");
            }
            else{
                var quantidadeLinhas = File.ReadLines(fileName).Count();
                cliente.Id = ++quantidadeLinhas;
                clientes.Add(cliente);
                string[] json = {JsonSerializer.Serialize(clientes)};
                await File.WriteAllLinesAsync(fileName, json);
                Console.WriteLine("Cliente cadastrado com sucesso");
            }
        }

        public List<Cliente> ObterTodosClientes(){
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

        public Cliente FiltrarClientePorNome(string nome) {
            var clientes = ObterTodosClientes();
            var cliente = clientes.Where(x => x.Nome == nome);
            return cliente.FirstOrDefault();
        }
        
        public Cliente FiltrarClientePorEmail(string email) {
            var clientes = ObterTodosClientes();
            var cliente = clientes.Where(x => x.Email == email);
            return cliente.FirstOrDefault();
        }

        public void AtualizarCliente()
        {

            string fileName = "Banco/Cliente.json";

            string json = File.ReadAllText(fileName);
            var jObject = JObject.Parse(json);
            JArray clientes = (JArray)jObject["clientes"];

            Console.WriteLine("Digite o id do cliente");

            var clienteId = Convert.ToInt32(Console.ReadLine());

            if (clienteId > 0)
            {
                Console.WriteLine("Digite o novo nome do Cliente: ");
                var nomeCliente = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Digite o novo email do Cliente: ");
                var emailCliente = Convert.ToString(Console.ReadLine());

                foreach (var cliente in clientes.Where(obj => obj["Id"].Value<int>() == clienteId))
                {
                    cliente["nomeCliente"] = !string.IsNullOrEmpty(nomeCliente) ? nomeCliente : "";
                    cliente["emailCliente"] = !string.IsNullOrEmpty(emailCliente) ? emailCliente : "";
                }

                jObject["clientes"] = clientes;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(fileName, output);
            }
            else
            {
                Console.WriteLine("Id inválido");
            }
        }
        
    }
}