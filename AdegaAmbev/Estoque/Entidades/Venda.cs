using AdegaAmbev.Comum.Entidades;
using AdegaAmbev.Comum.Enums;
using System.Collections.Generic;

namespace AdegaAmbev.Estoque.Entidades
{
    public class Venda: EntidadeBase // Verificar como fazer ID incremental.
    {
        protected Venda()
        {
        }
        
        public Venda(int clienteId, double valorTotal, List<VendaItem> itens, TipoVenda tipoVenda)
        {
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            Itens = itens;
            TipoVenda = tipoVenda;
        }

        public override string ToString()
        {
            string itensString = "";

            Itens.ForEach(x => itensString += $"{x} // ");

            return @$"
    Cliente Id = {ClienteId}
    Valor Total = {ValorTotal}
    Tipo Venda = {TipoVenda}
    Itens:
        {itensString}";
        }

        public int ClienteId { get; set; }
        public double ValorTotal { get; set; }
        public List<VendaItem> Itens { get; set; }
        public TipoVenda TipoVenda { get; set; }


    }
}
