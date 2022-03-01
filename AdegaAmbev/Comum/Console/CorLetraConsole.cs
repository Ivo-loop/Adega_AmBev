using System;

namespace AdegaAmbev.Comum
{
    public static class CorLetraConsole
    {
        public static void Vermelho()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public static void Verde()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void Preto()
        {
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void Azul()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}
