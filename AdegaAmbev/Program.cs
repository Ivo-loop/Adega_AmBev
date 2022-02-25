using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Repositorio;
using System;

namespace AdegaAmbev
{
    public class Program
    {
        static void Main(string[] args)
        {
            RepositorioProduto repositorio = new RepositorioProduto();

            repositorio.CadastrarProduto(new Produto("brahma", "alcolico", 14.9));
            var produtos = repositorio.BuscarTodosOsProdutos();
            foreach(var poduto in produtos)
            {
                Console.WriteLine(poduto);
            }


        }

        public static void StartupSnake()
        {
            // chamar intefaces aqui para inicializar.
        }
    }
}
