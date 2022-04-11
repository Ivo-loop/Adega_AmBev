using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using AdegaAmbev.Produtos.Service;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using AdegaAmbev.Produtos.Entidades;

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

            var estoque = new List<Estoque.Entidades.Estoque>
            {
                new Estoque.Entidades.Estoque(123, 15),
                new Estoque.Entidades.Estoque(456, 20)
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
            
            _estoqueRepository.ObterTodos().Returns(new List<Estoque.Entidades.Estoque>());

            // Act
            _estoqueService.VizualizarEstoque(_produtoService, true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("Nenhum item encontrado no estoque.\n\nAperte qualquer tecla para continuar...",
                Environment.NewLine)));
        }
    }
}
