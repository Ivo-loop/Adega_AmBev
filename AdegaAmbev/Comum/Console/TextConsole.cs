using System;

namespace AdegaAmbev.Comum
{
    public static class TextConsole
    {
        public static string AlinharAoCentro(string texto)
        {
            return string.Format("{0," + Console.WindowWidth / 2 + "}", texto);
        }
    }
}
