using AdegaAmbev.Comum.Enums;
using AdegaAmbev.Estoque.Entidades;
using AdegaAmbev.Estoque.Menu;
using AdegaAmbev.Estoque.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Service
{
    public static class VendaService
    {
        private static readonly EstoqueRepository _estoqueRepository = new();

        public static void MenuVenda()
        {
            Console.Clear();
            Console.WriteLine("1 - Realizar Venda");
            Console.WriteLine("0 - Voltar\n");
            Console.Write("Opção: ");


            switch (Console.ReadLine())
            {
                case "1":
                    RealizarVenda().Wait();
                    break;

                case "0":
                    GrupoDMenu.Iniciar();
                    break;
            }

            MenuVenda();
        }

        public static async Task RealizarVenda()
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

            var produtos = new List<Produto.Entidades.Produto>();
            var vendaItens = new List<VendaItem>();
            AdicionarItens(vendaItens, produtos);

            //var valorTotal = CalcularValorTotal(vendaItens, produtos);
            var valorTotal = 100.32;

            var venda = new Venda(codigoCliente, valorTotal, vendaItens, tipoVenda);
            await _estoqueRepository.DescontarEstoque(vendaItens);

            vendaRepository.Create(venda);
        }

        private static void AdicionarItens(List<VendaItem> itens, List<Produto.Entidades.Produto> produtos)
        {

            string adicionarNovoProduto;

            do
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

                Console.Write("Deseja inserir mais um item na venda? (s/n): ");
                adicionarNovoProduto = Console.ReadLine();
            }
            while (adicionarNovoProduto.ToUpper() == "S");
        }

        //private static double CalcularValorTotal(List<VendaItem> vendaItens, List<Produto.Entidades.Produto> produtos)
        //{
        //    double valorTotal = 0.0;

        //    Parallel.ForEach(vendaItens, item =>
        //    {
        //        var produto = produtos.SingleOrDefault(x => x.Id == item.ProdutoId);
        //        valorTotal += produto.Valor * Convert.ToDouble(item.Quantidade);
        //    });

        //    return valorTotal;
        //}
    }
}
