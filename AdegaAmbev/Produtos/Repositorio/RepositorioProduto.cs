using System;
using System.Collections.Generic;
using AdegaAmbev.Produtos.Entidades;

namespace AdegaAmbev.Produtos.Repositorio {
    public class RepositorioProduto
    {
        private readonly string Host {get; set;}

        public RepositorioProduto()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Produto.json";
        }        
        
        public void CadastrarProduto(){

        }

        public void AtualizarProduto(){
            
        }

        public List<Produto> BuscarTodosOsProdutos(){
            
        }

        public List<Produto> BuscarProdutosPorFiltros(string? nome, TipoBebida? tipoBebida){
            
        }

        public Produto BuscarProdutoPorId(int Id){
            
        }
    }
}