using AdegaAmbev.Comum;
using AdegaAmbev.Estoque.Menu;
using AdegaAmbev.Estoque.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Service
{
    public static class EstoqueService
    {
        private static readonly EstoqueRepository _estoqueRepository = new();

        public static void MenuEstoque()
        {
            Console.Clear();
            Console.WriteLine("1 - Inserir Estoque");
            Console.WriteLine("2 - Vizualizar Estoque");
            Console.WriteLine("3 - Vizualizar Estoque Por Produto");
            Console.WriteLine("0 - Voltar\n");
            Console.Write("Opção: ");


            switch (Console.ReadLine())
            {
                case "1":
                    InserirEstoque().Wait();
                    break;

                case "2":
                    CorConsole.Branco();
                    VizualizarEstoque().Wait();
                    Console.ResetColor();
                    break;

                case "3":
                    Console.Write("Digite o código do produto: ");
                    var codigoProduto = Convert.ToInt32(Console.ReadLine());
                    VizualizarEstoquePorProduto(codigoProduto).Wait();
                    Console.ResetColor();
                    break;

                case "0":
                    GrupoDMenu.Iniciar();
                    break;
            }

            MenuEstoque();
        }

        public static async Task InserirEstoque()
        {
            Console.Clear();

            Console.WriteLine("Bem vindo ao Menu para inserir ESTOQUE\n");
            
            Console.Write("Digite o código do produto: ");
            var codigoProduto = Convert.ToInt32(Console.ReadLine());
            //Verificar se o produto existe no futuro.

            Console.Write("Digite a quantidade: ");
            var quantidade = Convert.ToInt32(Console.ReadLine());
            if (!EhQuantidadeValida(quantidade))
            {
                return;
            }

            var estoque = new Entidades.Estoque(codigoProduto, quantidade);
            await _estoqueRepository.Create(estoque);

        }

        public static async Task VizualizarEstoque()
        {
            Console.Clear();
            var todosEstoquesSalvos = _estoqueRepository.ObterTodos();

            foreach(var estoque in todosEstoquesSalvos)
            {
                Console.Write($"Produto Id = {estoque.ProdutoId}");
                Console.Write($" Quantidade = {estoque.Quantidade}\n");
            }

            Console.Write("\nAperte qualquer tecla para continuar...");
            Console.ReadLine();
        }

        public static async Task VizualizarEstoquePorProduto(int codigoProduto)
        {
            Console.Clear();

            var estoque = _estoqueRepository.ObterPorCodigo(codigoProduto);

            if(estoque == null)
            {
                CorLetraConsole.Vermelho();
                Console.Write("Código de estoque não encontrado na base de dados.");
                Console.ResetColor();
                Thread.Sleep(3000);
                return;
            }

            CorConsole.Branco();

            Console.Write($"Produto Id = {estoque.ProdutoId}");
            Console.Write($" Quantidade = {estoque.Quantidade}\n");

            Console.Write("\nAperte qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private static bool EhQuantidadeValida(int codigo)
        {
            if (codigo <= 0)
            {
                Console.WriteLine($"\nA quantidade informada é inválida. Código precisa ser positivo");
                Console.WriteLine($"Não será realizada alteração no estoque.");
                Thread.Sleep(5000);
                return false;
            }
            return true;
        }
    }
}
