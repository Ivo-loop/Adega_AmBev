using AdegaAmbev.Clientes.Entidades;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
            string fileName = "Banco/Cliente.txt";
            var validação = ValidarCliente(cliente);

            if (validação != "sucesso")
                return;
            
            if (File.ReadAllLines(fileName).Any(line => line.Contains(cliente.Email))){
                Console.WriteLine("Cliente já cadastrado");
            }
            else{
                var quantidadeLinhas = File.ReadLines(fileName).Count();
                cliente.Id = ++quantidadeLinhas;
                string[] linhas = {JsonSerializer.Serialize(cliente)};
                await File.AppendAllLinesAsync(fileName, linhas);
            }
        }

        public Cliente FiltrarClientePorNome(Cliente[] clientes, string nome) {
            return (Cliente)clientes.Where(cliente => cliente.Nome == nome);
        }
        
        public Cliente FiltrarClientePorEmail(Cliente[] clientes, string email) {
            return (Cliente)clientes.Where(cliente => cliente.Email == email);
        }

        public void AtualizarCliente(string jsonFile)
        {
            string json = File.ReadAllText(jsonFile);

            var jObject = JObject.Parse(json);
            JArray clientes = (JArray)jObject["clientes"];
            Console.Write("Digite o id do cliente");
            var clienteId = Convert.ToInt32(Console.ReadLine());

            if (clienteId > 0)
            {
                Console.Write("Digite o novo nome do Cliente: ");
                var nomeCliente = Convert.ToString(Console.ReadLine());

                Console.Write("Digite o novo email do Cliente: ");
                var emailCliente = Convert.ToString(Console.ReadLine());

                foreach (var cliente in clientes.Where(obj => obj["Id"].Value<int>() == clienteId))
                {
                    cliente["nomeCliente"] = !string.IsNullOrEmpty(nomeCliente) ? nomeCliente : "";
                    cliente["emailCliente"] = !string.IsNullOrEmpty(emailCliente) ? emailCliente : "";
                }

                jObject["clientes"] = clientes;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, output);
            }
            else
            {
                Console.Write("Id inválido");
            }
        }
    }
}