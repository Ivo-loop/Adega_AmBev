using AdegaAmbev.Estoque.Entidades;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Repository
{
    public class EstoqueRepository
    {
        private string Host { get; set; }

        public EstoqueRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Estoque.json";
        }

        public async Task Create(Entidades.Estoque estoque)
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

            await File.WriteAllTextAsync(Host, JsonSerializer.Serialize(bancoSerializado));
        }

        public async Task<List<Entidades.Estoque>> ObterTodos()
        {
            var banco = File.ReadAllText(Host);

            if (banco == "")
            {
                return new List<Entidades.Estoque>();
            }

            var bancoSerializado = JsonSerializer.Deserialize<List<Entidades.Estoque>>(banco);
            return bancoSerializado;
        }

        public Entidades.Estoque ObterPorCodigo(int codigoEstoque)
        {
            var banco = File.ReadAllText(Host);

            if (banco == "")
            {
                return null;
            }

            var bancoSerializado = JsonSerializer.Deserialize<List<Entidades.Estoque>>(banco);
            return bancoSerializado.SingleOrDefault(x => x.ProdutoId == codigoEstoque);
        }

        public async Task DescontarEstoque(List<VendaItem> itens)
        {
            var banco = File.ReadAllText(Host);

            if (banco == "")
            {
                return;
            }

            var bancoSerializado = JsonSerializer.Deserialize<List<Entidades.Estoque>>(banco);

            foreach(var vendaItem in itens)
            {
                var estoque = bancoSerializado.SingleOrDefault(x => x.ProdutoId == vendaItem.ProdutoId);
                estoque.SubtrairQuantidade(vendaItem.Quantidade);
            }

            await File.WriteAllTextAsync(Host, JsonSerializer.Serialize(bancoSerializado));
        }
    }
}
