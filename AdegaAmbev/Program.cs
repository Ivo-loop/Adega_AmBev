using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.MenuProduto;
using System;

namespace AdegaAmbev
{
    public class Program
    {
        static void Main(string[] args)
        {
            var menu = new MenuProduto();

            menu.IniciarMenuProduto();            
        }

        public static void StartupSnake()
        {
            // chamar intefaces aqui para inicializar.
        }
    }
}
