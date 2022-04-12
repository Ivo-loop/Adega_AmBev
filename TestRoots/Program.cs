using TestRoots;

namespace ClienteTestRoots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tipoBebiba = new TipoBebidaServiceTeste();
            tipoBebiba.rodarTodosTestes();

            var clienteTest = new ClienteTest();
            clienteTest.RodarTestesCliente();

            var estoqueTest = new EstoqueTest();
            estoqueTest.ExcecutarTodosOsTestes();

            MenuTesteProduto.IniciarMenuProduto();
        }
    }
}
