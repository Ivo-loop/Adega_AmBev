using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AdegaAmbev.Estoque.Repository
{
    public class EstoqueRepository
    {
        public string Host { get; set; }

        public EstoqueRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Estoque.json";
        }

        public void Create(Entidades.Estoque estoque) 
        {
            var banco = File.ReadAllText(Host);
            if (banco == "")
            {
                File.WriteAllText(Host, JsonSerializer.Serialize(new List<Entidades.Estoque> { estoque }));
            }

            var bancoSerializado = JsonSerializer.Deserialize<List<Entidades.Estoque>>(banco);
            var estoqueSalvo = bancoSerializado.SingleOrDefault(x => x.ProdutoId == estoque.ProdutoId);

            if (estoqueSalvo != null)
            {
                estoqueSalvo.AtualizarQuantidade(estoque.Quantidade);
            }
            else
            {
                bancoSerializado.Add(estoque);
            }

            File.WriteAllText(Host, JsonSerializer.Serialize(bancoSerializado));
        }
    }
}
