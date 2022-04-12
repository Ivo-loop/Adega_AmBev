using AdegaAmbev.Estoque.Menu;
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
    public class GrupoDMenuTest
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
        public void EntrarNoModuloEstoque_QuandoOpcaoDigitadaFor1_DeveChamarModuloEstoque()
        {
            // Arrange
            var input = new StringReader("1");
            Console.SetIn(input);

            // Action
            GrupoDMenu.Iniciar(_estoqueService, _vendaService, true);

            //Assert
            _estoqueService.Received(1).MenuEstoque();
            _vendaService.DidNotReceive().MenuVenda();
        }

        [Test]
        public void EntrarNoModuloVenda_QuandoOpcaoDigitadaFor2_DeveChamarModuloVenda()
        {
            // Arrange
            var input = new StringReader("2");
            Console.SetIn(input);

            // Action
            GrupoDMenu.Iniciar(_estoqueService, _vendaService, true);

            //Assert
            _vendaService.Received(1).MenuVenda();
            _estoqueService.DidNotReceive().MenuEstoque();
        }

        [Test]
        public void EntrarNoModuloEstoque_QuandoOpcaoDigitadaForInvalidaEPosteriormente1_DeveChamarModuloEstoque()
        {
            // Arrange
            var input = new StringReader("@I#*@!Jdashidhas\n1");
            Console.SetIn(input);

            // Action
            GrupoDMenu.Iniciar(_estoqueService, _vendaService, true);

            //Assert
            _estoqueService.Received(1).MenuEstoque();
            _vendaService.DidNotReceive().MenuVenda();
        }

        [Test]
        public void EntrarNoModuloVenda_QuandoOpcaoDigitadaForInvalidaEPosteriormente2_DeveChamarModuloVenda()
        {
            // Arrange
            var input = new StringReader("@I#*@!Jdashidhas\n2");
            Console.SetIn(input);

            // Action
            GrupoDMenu.Iniciar(_estoqueService, _vendaService, true);

            //Assert
            _vendaService.Received(1).MenuVenda();
            _estoqueService.DidNotReceive().MenuEstoque();
        }

        [Test]
        public void NaoDeveEntrarEmNenhumModulo_QuandoOpcaoDigitadaFor0_DeveApenasRetornar()
        {
            // Arrange
            var input = new StringReader("0");
            Console.SetIn(input);

            // Action
            GrupoDMenu.Iniciar(_estoqueService, _vendaService, true);

            //Assert
            _estoqueService.DidNotReceive().MenuEstoque();
            _vendaService.DidNotReceive().MenuVenda();
        }
    }
}
