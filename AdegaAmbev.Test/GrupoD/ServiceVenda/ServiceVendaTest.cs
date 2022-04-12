using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using AdegaAmbev.Estoque.Entidades;
using AdegaAmbev.Comum.Enums;
using System.Threading.Tasks;

namespace AdegaAmbev.Test.GrupoD.ServiceVenda
{
    public class ServiceVendaTest
    {
        private EstoqueRepository _estoqueRepository;
        private VendaRepository _vendaRepository;
        private VendaService _vendaService;

        [SetUp]
        public void Setup()
        {
            _estoqueRepository = Substitute.For<EstoqueRepository>();
            _vendaRepository = Substitute.For<VendaRepository>();
            _vendaService = new VendaService(_estoqueRepository, _vendaRepository);
        }

        [Test]
        public void MostrarTodasAsVendas_QuandoExistirVendasRegistradas_DeveMostrarTodasAsVendas()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("");
            Console.SetIn(input);

            var itensVenda = new List<VendaItem>
            {
                new VendaItem(1, 5)
            };

            var vendas = new List<Venda>
            {
                new Venda(1, 15.0, itensVenda, TipoVenda.VENDA_DIRETA),
                new Venda(2, 30.0, itensVenda, TipoVenda.DELIVERY)
            };

            _vendaRepository.ObterTodos().Returns(vendas);

            // Act
            _vendaService.MostrarTodasAsVendas(true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("\r\n    Cliente Id = 1\r\n    Valor Total = 15\r\n    Tipo Venda = VENDA_DIRETA\r\n    Itens:\r\n        Produto Id = 1  Quantidade = 5 // \r\n" +
                "=====================================================\n\r\n\r\n    Cliente Id = 2\r\n    Valor Total = 30\r\n    Tipo Venda = DELIVERY\r\n    Itens:\r\n        " +
                "Produto Id = 1  Quantidade = 5 // \r\n=====================================================\n\r\n\nAperte qualquer tecla para continuar...",
                Environment.NewLine)));
        }

        [Test]
        public void MostrarTodasAsVendas_QuandoNaoExistirVendasRegistradas_DeveMostrarMensagemInformativa()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader("");
            Console.SetIn(input);

            _vendaRepository.ObterTodos().Returns(new List<Venda>());

            // Act
            _vendaService.MostrarTodasAsVendas(true);

            // Assert
            Assert.That(output.ToString(), Is.EqualTo(string
                .Format("Nenhuma venda registrada.\n\nAperte qualquer tecla para continuar...",
                Environment.NewLine)));
        }

        [Test]
        public async Task RealizarVenda_deve_realizar_uma_venda()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(@"1
1
1
1
N");
            Console.SetIn(input);

            _estoqueRepository.ObterPorCodigo(Arg.Any<int>()).Returns(
                new Estoque.Entidades.Estoque(1, 1));

            await _vendaService.RealizarVenda(false);

            await _estoqueRepository.Received().DescontarEstoque(Arg.Any<List<VendaItem>>());
            _vendaRepository.Received().Create(Arg.Any<Venda>());
        }
    }
}