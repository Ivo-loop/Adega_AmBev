using AdegaAmbev.Comum.Enums;
using AdegaAmbev.Estoque.Entidades;
using NUnit.Framework;
using System.Collections.Generic;

namespace AdegaAmbev.Test.GrupoD.Entidades
{
    public class EntityVendaTest
    {
        [Test]
        public void Instanciar_QuandoRecebeInformacaoValida_DeveInstanciarObjetoEManterValores()
        {
            // Arrange
            int clienteId = 10;
            double valorTotal = 10.5;
            List<VendaItem> itens = new() { new VendaItem(1, 2) };
            TipoVenda tipoVenda = TipoVenda.DELIVERY;

            // Act
            var venda = new Venda(clienteId, valorTotal, itens, tipoVenda);

            // Assert
            Assert.AreEqual(clienteId, venda.ClienteId);
            Assert.AreEqual(valorTotal, venda.ValorTotal);
            Assert.AreEqual(itens, venda.Itens);
            Assert.AreEqual(tipoVenda, venda.TipoVenda);
        }

        [Test]
        public void TranportarDadosParaStrings()
        {
            // Arrange
            string itensString = "";
            int clienteId = 10;
            double valorTotal = 10.5;
            List<VendaItem> itens = new() { new VendaItem(1, 2) };
            TipoVenda tipoVenda = TipoVenda.DELIVERY;

            // Act
            var venda = new Venda(clienteId, valorTotal, itens, tipoVenda);
            var resultado = venda.ToString();
            itens.ForEach(x => itensString += $"{x} // ");

            // Assert
            Assert.AreEqual($"\r\n    Cliente Id = {venda.ClienteId}\r\n    Valor Total = {venda.ValorTotal}\r\n    Tipo Venda = {venda.TipoVenda}\r\n    Itens:\r\n        {itensString}", resultado);
        }
    }
}