using System.IO;

namespace TestRoots.Common
{
    public class BancoUtils
    {
        private string Path { get; set; }
        public BancoUtils(string path)
        {
            Path = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\" + path;
            this.ClearBanco();
        }

        public void ClearBanco()
        {
            File.WriteAllText(Path, "");
        }
    }
}
