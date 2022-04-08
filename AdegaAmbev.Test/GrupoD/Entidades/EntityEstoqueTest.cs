using NUnit.Framework;

namespace AdegaAmbev.Test.GrupoD.Entidades
{
    public class EntityEstoqueTest
    {
        [TestCase(123, 10)]
        [TestCase(542, 33)]
        [TestCase(12, 123)]
        public void Instanciar_QuandoRecebeInformacaoValida_DeveInstanciarObjetoEManterValores(int codigoProduto, int quantidadeInicial)
        {
            // Arrange

            // Act
            var estoque = new Estoque.Entidades.Estoque(codigoProduto, quantidadeInicial);

            // Assert
            Assert.AreEqual(quantidadeInicial, estoque.Quantidade);
            Assert.AreEqual(codigoProduto, estoque.ProdutoId);
        }

        [TestCase(123, 10, 50)]
        [TestCase(542, 33, 3)]
        [TestCase(12, 123, 17)]
        public void AtualizarQuantidade_QuandoRecebeInformacaoValida_DeveAtualizarQuantidadeEstoque(int codigoProduto, int quantidadeInicial, int quantidadeAtualizada)
        {
            // Arrange
            var estoque = new Estoque.Entidades.Estoque(codigoProduto, quantidadeInicial);

            // Act
            estoque.AtualizarQuantidade(quantidadeAtualizada);

            // Assert
            Assert.AreEqual(quantidadeAtualizada, estoque.Quantidade);
            Assert.AreEqual(codigoProduto, estoque.ProdutoId);
        }

        [TestCase(123, 50, 10, 40)]
        [TestCase(542, 33, 3, 30)]
        [TestCase(12, 100, 17, 83)]
        public void SubtrairQuantidade_QuandoRecebeInformacaoValida_DeveSubtrairQuantidadeEstoque(int codigoProduto, int quantidadeInicial, int quantidadeSubtrair, int quantidadeFinal )
        {
            // Arrange
            var estoque = new Estoque.Entidades.Estoque(codigoProduto, quantidadeInicial);

            // Act
            estoque.SubtrairQuantidade(quantidadeSubtrair);

            // Assert
            Assert.AreEqual(quantidadeFinal, estoque.Quantidade);
            Assert.AreEqual(codigoProduto, estoque.ProdutoId);
        }
    }
}
