using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Service;
using AdegaAmbev.Utils.Interface;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using EstoqueEntity = AdegaAmbev.Estoque.Entidades.Estoque;

namespace AdegaAmbev.Test.GrupoD.ServiceEstoque
{
    public class ServiceEstoqueTest
    {
        private EstoqueRepository _estoqueRepository;
        private ProdutoService _produtoService;
        private EstoqueService _estoqueService;
        private IConsoleAgregator _consoleAgregator;

        [SetUp]
        public void Setup()
        {
            _estoqueRepository = Substitute.For<EstoqueRepository>();
            _produtoService = Substitute.For<ProdutoService>();
            _consoleAgregator = Substitute.For<IConsoleAgregator>();
            _estoqueService = new EstoqueService(_estoqueRepository, _consoleAgregator);
        }

        [Test]
        public void VisualizarEstoque_QuandoExistirItensNoEstoque_DeveMostrarTodosOsItensDoEstoque()
        {
            // Arrange
            var estoque = new List<EstoqueEntity>
            {
                new EstoqueEntity(123, 15),
                new EstoqueEntity(456, 20)
            };

            _estoqueRepository.ObterTodos().Returns(estoque);

            var produto1 = new Produto("Stella Artois", "Cerveja", 7.99);
            var produto2 = new Produto("Budweiser", "Cerveja", 5.99);
            _produtoService.GetId(123).Returns(produto1);
            _produtoService.GetId(456).Returns(produto2);

            // Act
            _estoqueService.VizualizarEstoque(_produtoService);

            // Assert
            _consoleAgregator.Received(1).Clear();

            _consoleAgregator.Received(1).Write(Arg.Is("Produto Id = 123 "));
            _consoleAgregator.Received(1).Write(Arg.Is("Nome Produto = Stella Artois "));
            _consoleAgregator.Received(1).Write(Arg.Is("Quantidade = 15\n"));

            _consoleAgregator.Received(1).Write(Arg.Is("Produto Id = 456 "));
            _consoleAgregator.Received(1).Write(Arg.Is("Nome Produto = Budweiser "));
            _consoleAgregator.Received(1).Write(Arg.Is("Quantidade = 20\n"));

            _consoleAgregator.Received(1).Write(Arg.Is("\nAperte qualquer tecla para continuar..."));
        }

        [Test]
        public void VisualizarEstoque_QuandoNaoExistirItensNoEstoque_DeveMostrarMensagemInformativa()
        {
            // Arrange
            _estoqueRepository.ObterTodos().Returns(new List<EstoqueEntity>());

            // Act
            _estoqueService.VizualizarEstoque(_produtoService);

            // Assert
            _consoleAgregator.Received(1).Clear();
            _consoleAgregator.Received(1).Write(Arg.Is("Nenhum item encontrado no estoque.\n"));
            _consoleAgregator.Received(1).Write(Arg.Is("\nAperte qualquer tecla para continuar..."));
        }

        [Test]
        public async Task InserirEstoque_QuandoChamado_DevePedirCodigoDoProdutoEQuantidadeEmEstoque()
        {
            //arrange
            var productCode = "2";
            var productQuantity = "3";
            _consoleAgregator.ReadLine().Returns(productCode, productQuantity);

            var expectWelcomeMenu = "Bem vindo ao Menu para inserir ESTOQUE\n";
            var expectCodigoProduto = "Digite o código do produto: ";
            var expectQuantity = "Digite a quantidade: ";

            _produtoService.ExisteProduto(Arg.Any<int>()).Returns(true);
            await _estoqueRepository.Create(Arg.Any<EstoqueEntity>());

            //act
            await _estoqueService.InserirEstoque(_produtoService);

            //assert
            _consoleAgregator.Received(1).Clear();
            _consoleAgregator.Received(1).WriteLine(Arg.Is(expectWelcomeMenu));
            _consoleAgregator.Received(1).Write(Arg.Is(expectCodigoProduto));
            _consoleAgregator.Received(1).Write(Arg.Is(expectQuantity));
            await _estoqueRepository.Received(1).Create(Arg.Any<EstoqueEntity>());
        }

        [Test]
        public async Task InserirEstoque_QuandoProdutoNaoExiste_NaoDeveInserirEstoque()
        {
            //arrange
            var productCode = "2";
            var productQuantity = "3";
            _consoleAgregator.ReadLine().Returns(productCode, productQuantity);

            var expectWelcomeMenu = "Bem vindo ao Menu para inserir ESTOQUE\n";
            var expectCodigoProduto = "Digite o código do produto: ";
            var expectError = "Produto não existe";

            _produtoService.ExisteProduto(Arg.Any<int>()).Returns(false);
            await _estoqueRepository.Create(Arg.Any<EstoqueEntity>());

            //act
            await _estoqueService.InserirEstoque(_produtoService);

            //assert
            _consoleAgregator.Received(1).Clear();
            _consoleAgregator.Received(1).WriteLine(Arg.Is(expectWelcomeMenu));
            _consoleAgregator.Received(1).Write(Arg.Is(expectCodigoProduto));
            _consoleAgregator.Received(1).WriteLine(Arg.Is(expectError));
            await _estoqueRepository.DidNotReceive().Create(Arg.Any<EstoqueEntity>());
        }

        [Test]
        public async Task InserirEstoque_QuandoProdutoExisteMasEstoqueInvalido_NaoDeveInserirEstoque()
        {
            //arrange
            var productCode = "2";
            var productQuantity = "0";

            _consoleAgregator.ReadLine().Returns(productCode, productQuantity);

            var expectWelcomeMenu = "Bem vindo ao Menu para inserir ESTOQUE\n";
            var expectCodigoProduto = "Digite o código do produto: ";
            var expectQuantity = "Digite a quantidade: ";
            var expectErrorOne = "\nA quantidade informada é inválida. Código precisa ser positivo";
            var expectErrorTwo = "Não será realizada alteração no estoque.";

            _produtoService.ExisteProduto(Arg.Any<int>()).Returns(true);
            await _estoqueRepository.Create(Arg.Any<EstoqueEntity>());

            //act
            await _estoqueService.InserirEstoque(_produtoService);

            //assert
            _consoleAgregator.Received(1).Clear();
            _consoleAgregator.Received(1).WriteLine(Arg.Is(expectWelcomeMenu));
            _consoleAgregator.Received(1).Write(Arg.Is(expectCodigoProduto));
            _consoleAgregator.Received(1).Write(Arg.Is(expectQuantity));
            _consoleAgregator.Received(1).WriteLine(Arg.Is(expectErrorOne));
            _consoleAgregator.Received(1).WriteLine(Arg.Is(expectErrorTwo));
            await _estoqueRepository.DidNotReceive().Create(Arg.Any<EstoqueEntity>());
        }

        [Test]
        public void VizualizarEstoquePorProduto_QuandoRecebeCodigoValido_DeveRetornarProduto()
        {
            // Arrange
            var estoque = new Estoque.Entidades.Estoque(111, 10);

            _estoqueRepository.ObterPorCodigo(111).Returns(estoque);

            var produto = new Produto("Vinho Rosé", "Vinhos", 49.90);
            _produtoService.GetId(111).Returns(produto);

        
            // Act
            _estoqueService.VizualizarEstoquePorProduto(_produtoService, 111);

            // Assert
            _consoleAgregator.Received(1).Write(Arg.Is("Produto Id = 111"));
            _consoleAgregator.Received(1).Write(Arg.Is(" Nome Produto = Vinho Rosé "));
            _consoleAgregator.Received(1).Write(Arg.Is("Quantidade = 10\n"));

            _consoleAgregator.Received(1).Write(Arg.Is("\nAperte qualquer tecla para continuar..."));
            _consoleAgregator.Received(1).Clear();
        }

        [Test]
        public void VizualizarEstoquePorProduto_QuandoRecebeCodigoInvalido_DeveRetornarErro()
        {
            // Arrange
            EstoqueEntity estoque = null;

            _estoqueRepository.ObterPorCodigo(111).Returns(estoque);

            // Act
            _estoqueService.VizualizarEstoquePorProduto(_produtoService, 111);

            // Assert
            _consoleAgregator.Received(1).Write(Arg.Is("Código de estoque não encontrado na base de dados."));
        }
    }
}
