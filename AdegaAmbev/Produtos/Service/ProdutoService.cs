using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AdegaAmbev.Produtos.Entidades;

namespace AdegaAmbev.Produtos.Service
{
    public class ProdutoService
    {
        private string Host { get; set; }
        private TipoBebidaService bebidaService { get; set; }

        public ProdutoService()
        {
            bebidaService = new TipoBebidaService();
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Produto.json";
        }

        public bool CadastrarProduto(Produto produto)
        {
            using FileStream stream = File.OpenRead(Host);
            if (bebidaService.ExisteTipoBebida(produto.TipoBebida))
                return false;

            var produtosDB = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();
            var qtdProdutos = produtosDB.Count;
            produto.SetId(++qtdProdutos);
            produtosDB.Add(produto);
            stream.Close();

            File.WriteAllText(Host, JsonSerializer.Serialize(produtosDB));
            return true;
        }

        public bool AtualizarProduto(int id, Produto produto)
        {
            using FileStream stream = File.OpenRead(Host);
            if (bebidaService.ExisteTipoBebida(produto.TipoBebida))
                return false;

            var produtosDb = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();
            var produtoDB = produtosDb.First(x => x.Id == id);
            var index = produtosDb.FindIndex(x => x.Id == id);
            produto.SetId(id);
            produtosDb[index] = produto;

            File.WriteAllText(Host, JsonSerializer.Serialize(produtosDb));
            return true;
        }

        public List<Produto> BuscarTodosOsProdutos()
        {
            using FileStream stream = File.OpenRead(Host);
            return JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
        }

        public Produto GetId(int id)
        {
            using FileStream stream = File.OpenRead(Host);
            var produtosDb = JsonSerializer.DeserializeAsync<List<Produto>>(stream).Result;
            stream.Close();
            return produtosDb.First(x => x.Id == id);
        }

        public List<Produto> BuscarProdutosPorFiltros(string nome = "", string tipoBebida = "")
        {
            using FileStream stream = File.OpenRead(Host);
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