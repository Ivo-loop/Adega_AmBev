using AdegaAmbev.Comum;
using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Produtos.Service;
using AdegaAmbev.Utils.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Service
{
    public class EstoqueService
    {
        private readonly EstoqueRepository _estoqueRepository = new();
        private readonly IConsoleAgregator _console;

        public EstoqueService(EstoqueRepository estoqueRepository, IConsoleAgregator consoleAgregator)
        {
            _estoqueRepository = estoqueRepository;
            _console = consoleAgregator;
        }

        public virtual void MenuEstoque()
        {
            ProdutoService produtoService = new ProdutoService();

            _console.Clear();
            _console.WriteLine("1 - Inserir Estoque");
            _console.WriteLine("2 - Vizualizar Estoque");
            _console.WriteLine("3 - Vizualizar Estoque Por Produto");
            _console.WriteLine("0 - Voltar\n");
            _console.Write("Opção: ");

            switch (_console.ReadLine())
            {
                case "1":
                    InserirEstoque(produtoService).Wait();
                    break;

                case "2":
                    _console.Branco();
                    VizualizarEstoque(produtoService);
                    _console.ResetColor();
                    break;

                case "3":
                    _console.Write("Digite o código do produto: ");
                    var codigoProduto = Convert.ToInt32(Console.ReadLine());
                    VizualizarEstoquePorProduto(produtoService, codigoProduto);
                    _console.ResetColor();
                    break;
                case "0":
                    return;
            }
            MenuEstoque();
        }

        public async Task InserirEstoque(ProdutoService produtos)
        {
            _console.Clear();
            _console.WriteLine("Bem vindo ao Menu para inserir ESTOQUE\n");
            _console.Write("Digite o código do produto: ");

            var codigoProduto = Convert.ToInt32(_console.ReadLine());

            if (!produtos.ExisteProduto(codigoProduto))
            {
                _console.WriteLine("Produto não existe");
                return;
            }

            _console.Write("Digite a quantidade: ");
            var quantidade = Convert.ToInt32(_console.ReadLine());
            if (!EhQuantidadeValida(quantidade))
            {
                return;
            }

            var estoque = new Entidades.Estoque(codigoProduto, quantidade);
            await _estoqueRepository.Create(estoque);
        }

        public void VizualizarEstoque(ProdutoService produtos)
        {
            _console.Clear();

            var todosEstoquesSalvos = _estoqueRepository.ObterTodos();

            if (todosEstoquesSalvos.Count != 0)
            {
                foreach (var estoque in todosEstoquesSalvos)
                {
                    var produto = produtos.GetId(estoque.ProdutoId);
                    _console.Write($"Produto Id = {estoque.ProdutoId} ");
                    _console.Write($"Nome Produto = {produto.Nome} ");
                    _console.Write($"Quantidade = {estoque.Quantidade}\n");
                }
            }
            else
            {
                _console.Write("Nenhum item encontrado no estoque.\n");
            }

            _console.Write("\nAperte qualquer tecla para continuar...");
            _console.ReadLine();
        }

        public void VizualizarEstoquePorProduto(ProdutoService produtos, int codigoProduto)
        {
            _console.Clear();

            var estoque = _estoqueRepository.ObterPorCodigo(codigoProduto);

            if (estoque == null)
            {
                CorLetraConsole.Vermelho();
                _console.Write("Código de estoque não encontrado na base de dados.");
                _console.ResetColor();
                Thread.Sleep(3000);
                return;
            }

            CorConsole.Branco();

            var produto = produtos.GetId(estoque.ProdutoId);
            _console.Write($"Produto Id = {estoque.ProdutoId}");
            _console.Write($" Nome Produto = {produto.Nome} ");
            _console.Write($"Quantidade = {estoque.Quantidade}\n");

            _console.Write("\nAperte qualquer tecla para continuar...");
            _console.ReadLine();
        }

        private bool EhQuantidadeValida(int codigo)
        {
            if (codigo <= 0)
            {
                _console.WriteLine($"\nA quantidade informada é inválida. Código precisa ser positivo");
                _console.WriteLine($"Não será realizada alteração no estoque.");
                Thread.Sleep(5000);
                return false;
            }
            return true;
        }
    }
}
