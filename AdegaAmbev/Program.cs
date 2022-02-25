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
