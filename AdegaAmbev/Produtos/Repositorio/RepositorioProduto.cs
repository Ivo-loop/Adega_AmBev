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
        private readonly string Host; 

        public RepositorioProduto()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Produto.json";
        }        
        
        public async Task CadastrarProduto(Produto produto){
            var produtoAnterior = getUltimoProduto();
            produtoAnterior.produtoAnterior = produto;
            string[] linhas = { JsonSerializer.Serialize(produto) };
            await File.WriteAllLinesAsync(Host, linhas);
        }

        public void AtualizarProduto(int index, Produto produtoAtualizado){
            var tempProduto = getProduto();

            for (int i = 1; i <= index; i++) {
                tempProduto = tempProduto.produtoAnterior;
                if(i == index)
                {
                    produtoAtualizado.produtoAnterior = tempProduto.produtoAnterior;
                    tempProduto = produtoAtualizado;
                }
            }
        }

        public List<Produto> BuscarTodosOsProdutos(){
            using FileStream stream = File.OpenRead(Host);
            var produtoDB = JsonSerializer.DeserializeAsync<Produto>(stream).Result;
            var _trem = new List<Produto>();

            var tempProd = produtoDB;
            while (tempProd is not null)
            {
                _trem.Add(tempProd);
                tempProd = tempProd.produtoAnterior;
            };
            return _trem;
        }

        private Produto getUltimoProduto()
        {
            using FileStream stream = File.OpenRead(Host);
            var produtoDB = JsonSerializer.DeserializeAsync<Produto>(stream).Result;

            var tempVagao = produtoDB;
            while (tempVagao?.produtoAnterior != null)
            {
                tempVagao = tempVagao.produtoAnterior;
            }
            return tempVagao;
        }

        private Produto getProduto()
        {
            using FileStream stream = File.OpenRead(Host);
            var produtoDB = JsonSerializer.DeserializeAsync<Produto>(stream).Result;
            return produtoDB;
        }

        public List<Produto> BuscarProdutosPorNome(string nome){
            var tempProduto = getProduto();
            List<Produto> listVagao = new List<Produto>();
            do
            {
                if (tempProduto.Nome == nome)
                    listVagao.Add(tempProduto);
                tempProduto = tempProduto.produtoAnterior;
            } while (tempProduto != null);
            return listVagao;
        }

        public List<Produto> BuscarProdutosPorTipoBebida(TipoBebida tipoBebida)
        {
            var tempProduto = getProduto();
            List<Produto> listVagao = new List<Produto>();
            do
            {
                if (tempProduto.TipoBebida == tipoBebida)
                    listVagao.Add(tempProduto);
                tempProduto = tempProduto.produtoAnterior;
            } while (tempProduto != null);
            return listVagao;
        }

        public List<Produto> BuscarProdutosPor(string nome, TipoBebida tipoBebida)
        {
            var tempProduto = getProduto();
            List<Produto> listVagao = new List<Produto>();
            do
            {
                if (tempProduto.TipoBebida == tipoBebida && tempProduto.Nome == nome)
                    listVagao.Add(tempProduto);
                tempProduto = tempProduto.produtoAnterior;
            } while (tempProduto != null);
            return listVagao;
        }

        public Produto BuscarProdutoPorId(int Id){
            var produtosDb = BuscarTodosOsProdutos();
            return produtosDb.FirstOrDefault(a => a.Id == Id);
        }
    }
}