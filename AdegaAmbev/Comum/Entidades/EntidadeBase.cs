namespace AdegaAmbev.Comum.Entidades
{
    public class EntidadeBase
    {
        public int Id { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
