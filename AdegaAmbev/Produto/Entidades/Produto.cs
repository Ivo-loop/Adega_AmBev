using AdegaAmbev.Comum.Entidades;

namespace AdegaAmbev.Produto.Entidades {
    public class Produto : EntidadeBase
    {
        public string Nome {get; set;}
        public TipoBebida TipoBebida {get; set;}
        public double Valor {get; set;}
    }
}