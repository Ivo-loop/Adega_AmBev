using AdegaAmbev.Comum;
using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Produtos.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Service
{
    public class EstoqueService
    {
        private readonly EstoqueRepository _estoqueRepository = new();

        public EstoqueService(EstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public void MenuEstoque()
        {
            ProdutoService produtoService = new ProdutoService();

            Console.Clear();
            Console.WriteLine("1 - Inserir Estoque");
            Console.WriteLine("2 - Vizualizar Estoque");
            Console.WriteLine("3 - Vizualizar Estoque Por Produto");
            Console.WriteLine("0 - Voltar\n");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    InserirEstoque(produtoService).Wait();
                    break;

                case "2":
                    CorConsole.Branco();
                    VizualizarEstoque(produtoService);
                    Console.ResetColor();
                    break;

                case "3":
                    Console.Write("Digite o código do produto: ");
                    var codigoProduto = Convert.ToInt32(Console.ReadLine());
                    VizualizarEstoquePorProduto(produtoService, codigoProduto);
                    Console.ResetColor();
                    break;
                case "0":
                    return;
            }
            MenuEstoque();
        }

        public async Task InserirEstoque(ProdutoService produtos)
        {
            Console.Clear();

            Console.WriteLine("Bem vindo ao Menu para inserir ESTOQUE\n");
            
            Console.Write("Digite o código do produto: ");
            var codigoProduto = Convert.ToInt32(Console.ReadLine());

            if (!produtos.ExisteProduto(codigoProduto))
            {
                Console.WriteLine("Produto não existe");
                return;
            }

            Console.Write("Digite a quantidade: ");
            var quantidade = Convert.ToInt32(Console.ReadLine());
            if (!EhQuantidadeValida(quantidade))
            {
                return;
            }

            var estoque = new Entidades.Estoque(codigoProduto, quantidade);
            await _estoqueRepository.Create(estoque);
        }

        public void VizualizarEstoque(ProdutoService produtos)
        {
            Console.Clear();
            var todosEstoquesSalvos = _estoqueRepository.ObterTodos();

            foreach(var estoque in todosEstoquesSalvos)
            { 
                var produto = produtos.GetId(estoque.ProdutoId);
                Console.Write($"Produto Id = {estoque.ProdutoId} ");
                Console.Write($"Nome Produto = {produto.Nome} ");
                Console.Write($"Quantidade = {estoque.Quantidade}\n");
            }

            Console.Write("\nAperte qualquer tecla para continuar...");
            Console.ReadLine();
        }

        public void VizualizarEstoquePorProduto(ProdutoService produtos, int codigoProduto)
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

            var produto = produtos.GetId(estoque.ProdutoId);
            Console.Write($"Produto Id = {estoque.ProdutoId}");
            Console.Write($" Nome Produto = {produto.Nome} ");
            Console.Write($"Quantidade = {estoque.Quantidade}\n");

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
