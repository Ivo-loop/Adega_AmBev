using NUnit.Framework;

namespace AdegaAmbev.Test.GrupoD
{
    public class EntityEstoqueTest
    {
        [Test]
        public void Instanciar_QuandoRecebeInformacaoValida_DeveInstanciarObjetoEManterValores()
        {
            // Arrange
            var codigoProduto = 123;
            var quantidadeInicial = 10;

            // Act
            var estoque = new Estoque.Entidades.Estoque(codigoProduto, quantidadeInicial);

            // Assert
            Assert.AreEqual(quantidadeInicial, estoque.Quantidade);
            Assert.AreEqual(codigoProduto, estoque.ProdutoId);
        }

        [Test]
        public void AtualizarQuantidade_QuandoRecebeInformacaoValida_DeveAtualizarQuantidadeEstoque()
        {
            // Arrange
            var codigoProduto = 123;
            var quantidadeInicial = 10;
            var estoque = new Estoque.Entidades.Estoque(codigoProduto, quantidadeInicial);
            var quantidadeAtualizada = 50;

            // Act
            estoque.AtualizarQuantidade(quantidadeAtualizada);

            // Assert
            Assert.AreEqual(quantidadeAtualizada, estoque.Quantidade);
            Assert.AreEqual(codigoProduto, estoque.ProdutoId);
        }

        [Test]
        public void SubtrairQuantidade_QuandoRecebeInformacaoValida_DeveSubtrairQuantidadeEstoque()
        {
            // Arrange
            var codigoProduto = 123;
            var quantidadeInicial = 50;
            var estoque = new Estoque.Entidades.Estoque(codigoProduto, quantidadeInicial);
            var quantidadeAtualizada = 10;

            // Act
            estoque.SubtrairQuantidade(quantidadeAtualizada);

            // Assert
            Assert.AreEqual(40, estoque.Quantidade);
            Assert.AreEqual(codigoProduto, estoque.ProdutoId);
        }
    }
}
