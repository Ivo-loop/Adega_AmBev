namespace AdegaAmbev.Estoque.Entidades
{
    public class Estoque
    {
        public int ProdutoId { get; protected set; }
        public int Quantidade { get; protected set; }

        protected Estoque()
        {
        }

        public Estoque(int produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public void AtualizarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }

        public void SubtrairQuantidade(int quantidade)
        {
            Quantidade -= quantidade;
        }
    }
}
