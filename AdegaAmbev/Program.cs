using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Repositorio;
using System;
using AdegaAmbev.Estoque.Menu;

namespace AdegaAmbev
{
    public class Program
    {

        static void Main(string[] args)
        {
            StartupSnake();
            IniciarMenuGrupoD();
        }

        public static void StartupSnake()
        {
        }

        public static void IniciarMenuGrupoD()
        {
            GrupoDMenu.Iniciar();
        }
    }
}
