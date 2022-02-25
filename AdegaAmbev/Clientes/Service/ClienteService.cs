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
            string fileName = "Banco/Cliente.json";
            var validação = ValidarCliente(cliente);

            if (validação != "sucesso")
                return;
            
            if (File.ReadAllLines(fileName).Any(linha => linha.Contains(cliente.Email))){
                Console.WriteLine("Cliente já cadastrado");
            }
            else{
                var quantidadeLinhas = File.ReadLines(fileName).Count();
                cliente.Id = ++quantidadeLinhas;
                string[] linhas = {JsonSerializer.Serialize(cliente)};
                await File.AppendAllLinesAsync(fileName, linhas);
                Console.WriteLine("Cliente cadastrado com sucesso");
            }
        }

        public Cliente FiltrarClientePorNome(string nome) {
            string fileName = "Banco/Cliente.json";
            var clientes = File.ReadAllLines(fileName);
            var cliente = clientes.Select(linha => linha.Contains(nome));
            //return (Cliente)clientes.Where(cliente => cliente.Nome == nome);
            Console.WriteLine();
            return new Cliente();
        }
        
        //public Cliente FiltrarClientePorEmail(Cliente[] clientes, string email) {
        //    return (Cliente)clientes.Where(cliente => cliente.Email == email);
        //}

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