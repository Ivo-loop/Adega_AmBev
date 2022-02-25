using AdegaAmbev.Comum.Entidades;

namespace AdegaAmbev.Produtos.Entidades {
    public class Produto : EntidadeBase
    {
        public string Nome {get; set;}
        public TipoBebida TipoBebida {get; set;}
        public double Valor {get; set;}

        public Produto produtoAnterior { get; set;}
    
    public Produto(string nome, TipoBebida tipoBebida, double valor)
    {
        Nome = nome;
        TipoBebida = tipoBebida;
        Valor = valor;
    }
  }
}