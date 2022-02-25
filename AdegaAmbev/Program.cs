using AdegaAmbev.Clientes.Menu;
using AdegaAmbev.Produtos.Entidades;
using System;
using AdegaAmbev.Estoque.Menu;


namespace AdegaAmbev
{
    public class Program
    {

        static void Main(string[] args)
        {
            IniciarMenuGrupoB();
            IniciarMenuGrupoD();
        }

        public static void IniciarMenuGrupoB()
        {
            MenuCliente.Menu();
        }

        public static void IniciarMenuGrupoD()
        {
            GrupoDMenu.Iniciar();
        }
    }
}
