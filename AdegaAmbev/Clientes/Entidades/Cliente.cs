using AdegaAmbev.Comum.Entidades;

namespace AdegaAmbev.Clientes.Entidades
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}