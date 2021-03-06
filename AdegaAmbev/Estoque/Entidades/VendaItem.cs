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

        public override string ToString()
        {
            return @$"Produto Id = {ProdutoId}  Quantidade = {Quantidade}";
        }

        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
