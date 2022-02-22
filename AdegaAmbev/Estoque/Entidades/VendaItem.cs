namespace AdegaAmbev.Estoque.Entidades
{
    public class VendaItem
    {

        protected VendaItem()
        {
        }

        public VendaItem(int produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
