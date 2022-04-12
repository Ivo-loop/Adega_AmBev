using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Service;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EstoqueEntity = AdegaAmbev.Estoque.Entidades.Estoque;

namespace AdegaAmbev.Test.GrupoD.ServiceEstoque
{
    public class ServiceEstoqueTest
    {
        private EstoqueRepository _estoqueRepository;
        private ProdutoService _produtoService;
        private EstoqueService _estoqueService;

        [SetUp]
        public void Setup()
        {
            _estoqueRepository = Substitute.For<EstoqueRepository>();
            _produtoService = Substitute.For<ProdutoService>();
            _estoqueService = new EstoqueService(_estoqueRepository);
        }

        [Test]
        public void VisualizarEstoque_QuandoExistirItensNoEstoque_DeveMostrarTodosOsItensDoEstoque()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("");
            Console.SetIn(input);

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
            _estoqueService.VizualizarEstoque(_produtoService, true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("Produto Id = 123 Nome Produto = Stella Artois Quantidade = 15\nProduto Id = 456 Nome Produto = Budweiser Quantidade = 20\n\nAperte qualquer tecla para continuar...",
                Environment.NewLine)));
        }

        [Test]
        public void VisualizarEstoque_QuandoNaoExistirItensNoEstoque_DeveMostrarMensagemInformativa()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("");
            Console.SetIn(input);

            _estoqueRepository.ObterTodos().Returns(new List<EstoqueEntity>());

            // Act
            _estoqueService.VizualizarEstoque(_produtoService, true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("Nenhum item encontrado no estoque.\n\nAperte qualquer tecla para continuar...",
                Environment.NewLine)));
        }

        [Test]
        public async Task InserirEstoque_QuandoChamado_DevePedirCodigoDoProdutoEQuantidadeEmEstoque()
        {
            //arrange
            var outPut = new StringWriter();
            Console.SetOut(outPut);

            var productCode = "2";
            var productQuantity = "3";

            Console.SetIn(new StringReader($"{productCode}\n{productQuantity}"));

            var expectWelcomeMenu = "Bem vindo ao Menu para inserir ESTOQUE\n";
            var expectCodigoProduto = "Digite o código do produto: ";
            var expectQuantity = "Digite a quantidade: ";

            _produtoService.ExisteProduto(Arg.Any<int>()).Returns(true);
            await _estoqueRepository.Create(Arg.Any<EstoqueEntity>());

            //act
            await _estoqueService.InserirEstoque(_produtoService, false);

            //assert
            Assert.That(outPut.ToString(), Is.EqualTo($"{expectWelcomeMenu}\r\n{expectCodigoProduto}{expectQuantity}"));
            await _estoqueRepository.Received(1).Create(Arg.Any<EstoqueEntity>());
        }

        [Test]
        public async Task InserirEstoque_QuandoProdutoNaoExiste_NaoDeveInserirEstoque()
        {
            //arrange
            var outPut = new StringWriter();
            Console.SetOut(outPut);

            var productCode = "2";
            var productQuantity = "3";

            Console.SetIn(new StringReader($"{productCode}\n{productQuantity}"));

            var expectWelcomeMenu = "Bem vindo ao Menu para inserir ESTOQUE\n";
            var expectCodigoProduto = "Digite o código do produto: ";
            var expectError = "Produto não existe";

            _produtoService.ExisteProduto(Arg.Any<int>()).Returns(false);
            await _estoqueRepository.Create(Arg.Any<EstoqueEntity>());

            //act
            await _estoqueService.InserirEstoque(_produtoService, false);

            //assert
            Assert.That(outPut.ToString(), Is.EqualTo($"{expectWelcomeMenu}\r\n{expectCodigoProduto}{expectError}\r\n"));
            await _estoqueRepository.DidNotReceive().Create(Arg.Any<EstoqueEntity>());
        }

        [Test]
        public async Task InserirEstoque_QuandoProdutoExisteMasEstoqueInvalido_NaoDeveInserirEstoque()
        {
            //arrange
            var outPut = new StringWriter();
            Console.SetOut(outPut);

            var productCode = "2";
            var productQuantity = "0";

            Console.SetIn(new StringReader($"{productCode}\n{productQuantity}"));

            var expectWelcomeMenu = "Bem vindo ao Menu para inserir ESTOQUE\n";
            var expectCodigoProduto = "Digite o código do produto: ";
            var expectQuantity = "Digite a quantidade: ";
            var expectErrorOne = "A quantidade informada é inválida. Código precisa ser positivo";
            var expectErrorTwo = "Não será realizada alteração no estoque.";

            _produtoService.ExisteProduto(Arg.Any<int>()).Returns(true);
            await _estoqueRepository.Create(Arg.Any<EstoqueEntity>());

            //act
            await _estoqueService.InserirEstoque(_produtoService, false);

            //assert
            Assert.That(outPut.ToString(), Is.EqualTo($"{expectWelcomeMenu}\r\n{expectCodigoProduto}{expectQuantity}\n{expectErrorOne}\r\n{expectErrorTwo}\r\n"));
            await _estoqueRepository.DidNotReceive().Create(Arg.Any<EstoqueEntity>());
        }

        [Test]
        public void VizualizarEstoquePorProduto_QuandoRecebeCodigoValido_DeveRetornarProduto()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("");
            Console.SetIn(input);

            var estoque = new Estoque.Entidades.Estoque(111, 10);

            _estoqueRepository.ObterPorCodigo(111).Returns(estoque);

            var produto = new Produto("Vinho Rosé", "Vinhos", 49.90);
            _produtoService.GetId(111).Returns(produto);

        
            // Act
            _estoqueService.VizualizarEstoquePorProduto(_produtoService, 111, true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("Produto Id = 111 Nome Produto = Vinho Rosé Quantidade = 10\n\nAperte qualquer tecla para continuar...",
                Environment.NewLine)));
        }

        [Test]
        public void VizualizarEstoquePorProduto_QuandoRecebeCodigoInvalido_DeveRetornarErro()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            EstoqueEntity estoque = null;

            _estoqueRepository.ObterPorCodigo(111).Returns(estoque);

            // Act
            _estoqueService.VizualizarEstoquePorProduto(_produtoService, 111, true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("Código de estoque não encontrado na base de dados.",
                Environment.NewLine)));
        }
    }
}
