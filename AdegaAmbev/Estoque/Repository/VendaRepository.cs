using AdegaAmbev.Estoque.Entidades;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AdegaAmbev.Estoque.Repository
{
    public class VendaRepository
    {
        private string Host { get; set; }

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
            var maiorId = bancoSerializado.Max(x => x.Id);
            venda.SetId(maiorId + 1);
            bancoSerializado.Add(venda);
            File.WriteAllText(Host, JsonSerializer.Serialize(bancoSerializado));
        }

        public List<Venda> ObterTodos()
        {
            var banco = File.ReadAllText(Host);

            if (banco == "")
            {
                return new List<Venda>();
            }

            var bancoSerializado = JsonSerializer.Deserialize<List<Venda>>(banco);
            return bancoSerializado;
        }
    }
}
