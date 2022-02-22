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

        public int ClienteId { get; set; }
        public double ValorTotal { get; set; }
        public List<VendaItem> Itens { get; set; }
        public TipoVenda TipoVenda { get; set; }
    }
}
