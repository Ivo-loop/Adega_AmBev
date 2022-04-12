using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdegaAmbev.Test.GrupoD.Menu
{
    internal class EstoqueServiceMenuTest
    {
        private EstoqueService _estoqueService;
        private VendaService _vendaService;
        private EstoqueRepository _estoqueRepository;
        private VendaRepository _vendaRepository;

        [SetUp]
        public void Setup()
        {
            _estoqueRepository = Substitute.For<EstoqueRepository>();
            _vendaRepository = Substitute.For<VendaRepository>();
            _estoqueService = Substitute.For<EstoqueService>(_estoqueRepository);
            _vendaService = Substitute.For<VendaService>(_estoqueRepository, _vendaRepository);
        }

        [Test]
        public void EntrarNoModuloInserirEstoque_QuandoOpcaoDigitadaFor1_DeveChamarInserirEstoque()
        {
            // Arrange
            var input = new StringReader("1");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.Received(1);
            _vendaService.DidNotReceive();
        }

        [Test]
        public void EntrarNoModuloInserirEstoque_QuandoOpcaoDigitadaForInvalida1_DeveChamarInserirEstoque()
        {
            // Arrange
            var input = new StringReader("@I#*@!Jdashidhas\n1");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.Received(1);
            _vendaService.DidNotReceive();
        }

        [Test]
        public void EntrarNoModuloVizualizarEstoque_QuandoOpcaoDigitadaFor2_DeveChamarVizualizarEstoque()
        {
            // Arrange
            var input = new StringReader("2");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.Received(1);
            _vendaService.DidNotReceive();
        }

        [Test]
        public void EntrarNoModuloVizualizarEstoque_QuandoOpcaoDigitadaForInvalida2_DeveChamarVizualizarEstoque()
        {
            // Arrange
            var input = new StringReader("@I#*@!Jdashidhas\n2");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.Received(1);
            _vendaService.DidNotReceive();
        }

        [Test]
        public void EntrarNoModuloVizualizarEstoquePorProduto_QuandoOpcaoDigitadaFor3_DeveChamarVizualizarEstoquePorProduto()
        {
            // Arrange
            var input = new StringReader("3");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.Received(1);
            _vendaService.DidNotReceive();
        }

        [Test]
        public void EntrarNoModuloVizualizarEstoquePorProduto_QuandoOpcaoDigitadaForInvalida3_DeveChamarVizualizarPorProdutoEstoque()
        {
            // Arrange
            var input = new StringReader("@I#*@!Jdashidhas\n3");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.Received(1);
            _vendaService.DidNotReceive();
        }

        [Test]
        public void NaoDeveEntrarEmNenhumModulo_QuandoOpcaoDigitadaFor0_DeveApenasRetornar()
        {
            // Arrange
            var input = new StringReader("0");
            Console.SetIn(input);

            // Action
            _estoqueService.MenuEstoque();

            //Assert
            _estoqueService.DidNotReceive();
            _vendaService.DidNotReceive();
        }
    }
}
