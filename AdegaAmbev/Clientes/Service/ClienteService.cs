using AdegaAmbev.Clientes.Entidades;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
    }
}