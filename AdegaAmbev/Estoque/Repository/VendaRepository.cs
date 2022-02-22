using AdegaAmbev.Estoque.Entidades;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AdegaAmbev.Estoque.Repository
{
    public class VendaRepository
    {
        public string Host { get; set; }

        public VendaRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Venda.json";
        }

        public void Create(Venda venda)
        {
            var banco = File.ReadAllText(Host);

            if (banco == "")
            {
                File.WriteAllText(Host, JsonSerializer.Serialize(new List<Venda> { venda }));
                return;
            }
            
            var bancoSerializado = JsonSerializer.Deserialize<List<Venda>>(banco);
            File.WriteAllText(Host, JsonSerializer.Serialize(bancoSerializado));

        }
    }
}
