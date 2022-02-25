using AdegaAmbev.Clientes.Menu;


namespace AdegaAmbev
{
    public class Program
    {
        static void Main(string[] args)
        {
            StartupSnake();
        }

        public static void StartupSnake()
        {
            // chamar intefaces aqui para inicializar.
            MenuCliente.Menu();
        }
    }
}
