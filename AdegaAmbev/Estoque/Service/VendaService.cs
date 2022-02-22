using AdegaAmbev.Comum.Enums;
using AdegaAmbev.Estoque.Entidades;
using AdegaAmbev.Estoque.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Service
{
    public static class VendaService
    {

        public static void RealizarVenda()
        {
            Console.Clear();

            var vendaRepository = new VendaRepository();

            Console.WriteLine("Bem vindo ao menu para realizar VENDA\n");

            Console.Write("Digite o código do cliente: ");
            var codigoCliente = Convert.ToInt32(Console.ReadLine());
            //Verificar se o cliente existe no futuro.
            
            Console.Write("Digite o tipo da venda do cliente: ");
            // Fazer menu para tipo venda.
            var tipoVenda = (TipoVenda)Convert.ToInt32(Console.ReadLine());

            var produtos = new List<Produto>();
            var vendaItens = new List<VendaItem>();
            AdicionarItens(vendaItens, produtos);

            var valorTotal = CalcularValorTotal(vendaItens, produtos);
            var venda = new Venda(codigoCliente, valorTotal, vendaItens, tipoVenda);

        }

        private static void AdicionarItens(List<VendaItem> itens, List<Produto> produtos)
        {

            var adicionarNovoProduto = "S";

            while(adicionarNovoProduto.ToUpper() == "S")
            {
                Console.Write("Digite o código do produto: ");
                var codigoProduto = Convert.ToInt32(Console.ReadLine());
                //Buscar o produto com esse código.

                //produtos.Add(produtoSalvo);

                //Mostrar na tela o nome do produto.
                //Mostrar na tela o preço do produto.

                Console.Write("Digite a quantidade que deseja: ");
                var quantidadeProduto = Convert.ToInt32(Console.ReadLine());
                //validar se quantidade é válida

                var vendaItem = new VendaItem(codigoProduto, quantidadeProduto);
                itens.Add(vendaItem);

                Console.WriteLine("Deseja inserir mais um item na venda? s/n");
                adicionarNovoProduto = Console.ReadLine();
            }
            
        }

        private static double CalcularValorTotal(List<VendaItem> vendaItens, List<Produto> produtos)
        {
            var valorTotal = 0;

            Parallel.ForEach(vendaItens, item =>
            {
                var produto = produtos.SingleOrDefault(x => x.Id == item.ProdutoId);
                valorTotal += produto.Valor * item.Quantidade;
            });

            return valorTotal;
        }
    }
}
