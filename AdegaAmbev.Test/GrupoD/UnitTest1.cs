//using AdegaAmbev.Estoque.Repository;
//using AdegaAmbev.Estoque.Service;
//using AdegaAmbev.Produtos.Service;
//using NSubstitute;
//using NUnit.Framework;
//using System.Threading.Tasks;

//namespace AdegaAmbev.Test
//{
//    public class Tests
//    {

//        private EstoqueRepository _estoqueRepository;
//        private ProdutoService _produtoService;
//        private EstoqueService _estoqueService;

//        [SetUp]
//        public void Setup()
//        {
//            _estoqueRepository = Substitute.For<EstoqueRepository>();
//            _produtoService = Substitute.For<ProdutoService>();
//            _estoqueService = new EstoqueService(_estoqueRepository);
//        }

//        [Test]
//        public async Task Test1()
//        {
//            // Arrange


//            // Act
//            await _estoqueService.InserirEstoque(_produtoService);

//            // Assert
//        }
//    }
//}