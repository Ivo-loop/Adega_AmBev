using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AdegaAmbev.Comum;
using AdegaAmbev.Produtos.Entidades;

namespace AdegaAmbev.Produtos.Service
{
    public class ProdutoService
    {
        private string pathFile { get; set; }
        private TipoBebidaService bebidaService { get; set; }

        public ProdutoService()
        {
            bebidaService = new TipoBebidaService();
            pathFile = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Produto.json";
        }

        public bool CadastrarProduto(Produto produto)
        {
            
            if (!bebidaService.ExisteTipoBebida(produto.TipoBebida)) {
                CorLetraConsole.Vermelho();
                System.Console.WriteLine("Se é engraçadinho né? Agora vai colocar um tipo bebida que existe na base");
                CorLetraConsole.Preto();
                return false; 
            }

            using FileStream stream = File.OpenRead(pathFile);
            var produtosDB = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();

            var qtdProdutos = produtosDB.Count;
            produto.Id = qtdProdutos + 1;
            produtosDB.Add(produto);

            File.WriteAllText(pathFile, JsonSerializer.Serialize(produtosDB));
            return true;
        }

        public bool AtualizarProduto(int id, Produto produto)
        {
            if (!bebidaService.ExisteTipoBebida(produto.TipoBebida)) {
                CorLetraConsole.Vermelho();
                System.Console.WriteLine("Se é engraçadinho né? Agora vai colocar um tipo bebida que existe na base");
                CorLetraConsole.Preto();
                return false; 
            }

            using FileStream stream = File.OpenRead(pathFile);
            var produtosDb = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();

            var produtoDB = produtosDb.First(x => x.Id == id);
            var index = produtosDb.FindIndex(x => x.Id == id);

            produto.Id = id;
            produtosDb[index] = produto;

            File.WriteAllText(pathFile, JsonSerializer.Serialize(produtosDb));
            return true;
        }

        public List<Produto> BuscarTodosOsProdutos()
        {
            using FileStream stream = File.OpenRead(pathFile);
            return JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
        }

        public virtual Produto GetId(int id)
        {
            using FileStream stream = File.OpenRead(pathFile);
            var produtosDb = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();

            return produtosDb.First(x => x.Id == id);
        }

        public virtual bool ExisteProduto(int id)
        {
            using FileStream stream = File.OpenRead(pathFile);
            var produtosDb = JsonSerializer.DeserializeAsync<List<TipoBebida>>(stream).Result;
            stream.Close();
            return produtosDb.Any(x => x.Id == id);
        }

        public List<Produto> BuscarProdutosPorFiltros(string nome = "", string tipoBebida = "")
        {
            using FileStream stream = File.OpenRead(pathFile);
            var produtosDB = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();

            if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(tipoBebida))
            {
                return produtosDB.Where(x => x.Nome == nome && x.TipoBebida == tipoBebida).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(nome))
            {
                return produtosDB.Where(x => x.Nome == nome).ToList();
            }
            else
            {
                return produtosDB.Where(x => x.TipoBebida == tipoBebida).ToList();
            }
        }
    }
}