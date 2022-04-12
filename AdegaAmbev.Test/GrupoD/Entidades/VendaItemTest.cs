using NUnit.Framework;

namespace AdegaAmbev.Test.GrupoD.Entidades
{
    public class VendaItemTest
    {
        [TestCase(123, 10)]
        [TestCase(542, 33)]
        [TestCase(12, 123)]
        public void Instanciar_OProdutoParaVenda(int codigoProduto, int quantidadeInicial)
        {
            // Arrange

            // Act
            var vendaItem = new Estoque.Entidades.VendaItem(codigoProduto, quantidadeInicial);

            // Assert
            Assert.AreEqual(quantidadeInicial, vendaItem.Quantidade);
            Assert.AreEqual(codigoProduto, vendaItem.ProdutoId);
        }

        [TestCase(123, 10)]
        [TestCase(542, 33)]
        [TestCase(12, 123)]
        public void TranportarDadosParaStrings(int codigoProduto, int quantidadeInicial)
        {
            // Arrange
            var vendaItem = new Estoque.Entidades.VendaItem(codigoProduto, quantidadeInicial);

            // Act
            var resultado = vendaItem.ToString();

            // Assert
            Assert.AreEqual(@$"Produto Id = {vendaItem.ProdutoId}  Quantidade = {vendaItem.Quantidade}", resultado);
        }
    }
}
