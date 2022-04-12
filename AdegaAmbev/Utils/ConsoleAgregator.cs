using AdegaAmbev.Utils.Interface;
using System;

namespace AdegaAmbev.Utils
{
    public class ConsoleAgregator : IConsoleAgregator
    {
        public virtual void Write(string value) => Console.Write(value);

        public virtual void WriteLine(string value) => Console.WriteLine(value);

        public virtual string ReadLine() => Console.ReadLine();

        public virtual void Clear() => Console.Clear();

        public virtual void Branco()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public virtual void ResetColor() => Console.ResetColor();

        public void SetTitle(string title) => Console.Title = title;
    }
}
