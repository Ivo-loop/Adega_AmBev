using AdegaAmbev.Estoque.Menu;
using System;
using System.Runtime.InteropServices;

namespace AdegaAmbev
{
    public class Program
    {

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //private static extern IntPtr GetConsoleWindow();
        //private static readonly IntPtr ThisConsole = GetConsoleWindow();
        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        //private const int MAXIMIZE = 3;

        static void Main(string[] args)
        {
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            //ShowWindow(ThisConsole, MAXIMIZE);
            StartupSnake();
        }

        public static void StartupSnake()
        {
            GrupoDMenu.Iniciar();
        }
    }
}
