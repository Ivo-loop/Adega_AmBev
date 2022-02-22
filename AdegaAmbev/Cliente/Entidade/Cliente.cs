using System;
using AdegaAmbev.Comum.Entidade;

namespace AdegaAmbev.Cliente.Entidade
{
    public class Cliente : EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}