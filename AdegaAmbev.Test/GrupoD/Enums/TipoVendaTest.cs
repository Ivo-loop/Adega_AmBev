using AdegaAmbev.Comum.Enums;
using AdegaAmbev.Estoque.Entidades;
using NUnit.Framework;
using System.Collections.Generic;

namespace AdegaAmbev.Test.GrupoD.Enums
{
    public class TipoVendaTest
    {
        [TestCase(1)]
        [TestCase(2)]
        public void UsandoEnunsDiferentes(TipoVenda meioVenda)
        {
            // Arrange
            int clienteId = 10;
            double valorTotal = 10.5;
            List<VendaItem> itens = new() { new VendaItem(1, 2) };
            TipoVenda tipoVenda = meioVenda;

            // Act
            var venda = new Venda(clienteId, valorTotal, itens, tipoVenda);

            // Assert
            Assert.AreEqual(clienteId, venda.ClienteId);
            Assert.AreEqual(valorTotal, venda.ValorTotal);
            Assert.AreEqual(itens, venda.Itens);
            Assert.AreEqual(tipoVenda, venda.TipoVenda);
        }
    }
}
