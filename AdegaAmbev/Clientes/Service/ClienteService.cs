using AdegaAmbev.Clientes.Entidades;

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

        public void CadastrarCliente(Cliente cliente){
            var validação = ValidarCliente(cliente);

            if (validação != "sucesso")
                return;
            
            /*todo: verificar se o cliente existe
            if (cliente existe){
                Console.WriteLine("Cliente já cadastrado")
            }
            else{
                salvar o cliente no banco
            }
            */
        }
    }
}