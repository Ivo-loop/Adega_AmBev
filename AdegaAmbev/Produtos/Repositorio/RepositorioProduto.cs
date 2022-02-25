using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdegaAmbev.Produtos.Entidades;

namespace AdegaAmbev.Produtos.Repositorio {
    public class RepositorioProduto
    {
        private string Host {get; set;}

        public RepositorioProduto()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Produto.json";
        }        
        
        public void CadastrarProduto(Produto produto){
            using FileStream stream = File.OpenRead(Host);
            var produtosDB = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();
            produto.Id = produtosDB.Count+1;
            produtosDB.Add(produto);
            stream.Close();

            File.WriteAllText(Host, JsonSerializer.Serialize(produtosDB));
        }

        public void AtualizarProduto(int id, Produto produto){
            using FileStream stream = File.OpenRead(Host);
            var produtosDb = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();
            var produtoDB = produtosDb.First(x => x.Id == id);
            var index = produtosDb.FindIndex(x => x.Id == id);

            produtosDb[index] = produto;

            File.WriteAllText(Host, JsonSerializer.Serialize(produtosDb));
        }

        public List<Produto> BuscarTodosOsProdutos(){
            using FileStream stream = File.OpenRead(Host);
            return JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
        }

        public List<Produto> BuscarProdutosPorFiltros(string nome, string tipoBebida){
            using FileStream stream = File.OpenRead(Host);
            var produtosDB = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();

            if (!String.IsNullOrWhiteSpace(nome) && tipoBebida != null)
            {
                return produtosDB.Where(x=> x.Nome == nome && x.TipoBebida == tipoBebida).ToList();
            }
            else if (!String.IsNullOrWhiteSpace(nome))
            {
                return produtosDB.Where(x => x.Nome == nome).ToList();
            }
            else
            {
                return produtosDB.Where(x=> x.TipoBebida == tipoBebida).ToList();
            }
        }
    }
}