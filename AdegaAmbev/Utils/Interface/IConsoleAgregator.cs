namespace AdegaAmbev.Utils.Interface
{
    public interface IConsoleAgregator
    {
        void Write(string value);

        void WriteLine(string value);

        string ReadLine();

        void Clear();

        void Branco();

        void ResetColor();

        void SetTitle(string title);
    }
}
