using AdegaAmbev.Comum;
using AdegaAmbev.Comum.Enums;
using AdegaAmbev.Estoque.Entidades;
using AdegaAmbev.Estoque.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AdegaAmbev.Estoque.Service
{
    public class VendaService
    {
        private readonly EstoqueRepository _estoqueRepository = new();
        private readonly VendaRepository _vendaRepository = new();

        public VendaService(EstoqueRepository estoqueRepository, VendaRepository vendaRepository)
        {
            _estoqueRepository = estoqueRepository;
            _vendaRepository = vendaRepository;
        }

        public virtual void MenuVenda()
        {
            Console.Clear();
            Console.WriteLine("1 - Realizar Venda");
            Console.WriteLine("2 - Mostrar Todas as Vendas");
            Console.WriteLine("0 - Voltar\n");
            Console.Write("Opção: ");


            switch (Console.ReadLine())
            {
                case "1":
                    RealizarVenda().Wait();
                    break;

                case "2":
                    MostrarTodasAsVendas();
                    break;

                case "0":
                    return;
            }

            MenuVenda();
        }

        public async Task RealizarVenda(bool clear = true)
        {
            if (clear)
                Console.Clear();

            Console.WriteLine("Bem vindo ao menu para realizar VENDA\n");

            Console.Write("Digite o código do cliente: ");
            var codigoCliente = Convert.ToInt32(Console.ReadLine());
            //Verificar se o cliente existe no futuro.
            
            Console.WriteLine("Digite o tipo da venda do cliente");
            MostrarCodigosVendas();
            Console.WriteLine("");
            var tipoVenda = (TipoVenda)Convert.ToInt32(Console.ReadLine());

            var produtos = new List<Produtos.Entidades.Produto>();
            var vendaItens = new List<VendaItem>();
            AdicionarItens(vendaItens, produtos);

            //var valorTotal = CalcularValorTotal(vendaItens, produtos);
            var valorTotal = 100.32;

            var venda = new Venda(codigoCliente, valorTotal, vendaItens, tipoVenda);
            await _estoqueRepository.DescontarEstoque(vendaItens);

            _vendaRepository.Create(venda);
        }

        public void MostrarTodasAsVendas([Optional] bool testes)
        {
            if (!testes)
                Console.Clear();

            var todasVendasSalvas = _vendaRepository.ObterTodos();

            if (todasVendasSalvas.Count != 0)
            {
                foreach (var venda in todasVendasSalvas)
                {
                    Console.WriteLine($"{venda}");
                    Console.WriteLine($"=====================================================\n");
                }
            }
            else
            {
                Console.Write("Nenhuma venda registrada.\n");
            }

            Console.Write("\nAperte qualquer tecla para continuar...");
            Console.ReadLine();
        }

        private void AdicionarItens(List<VendaItem> itens, List<Produtos.Entidades.Produto> produtos)
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
                var estoqueSalvo = _estoqueRepository.ObterPorCodigo(codigoProduto);

                var quantidadeProduto = Convert.ToInt32(Console.ReadLine());
                if (!EhQuantidadeValida(quantidadeProduto, estoqueSalvo))
                {
                    adicionarNovoProduto = "S";
                    continue;
                }

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

        private static bool EhQuantidadeValida(int quantidade, Entidades.Estoque estoqueSavlo)
        {
            if (quantidade <= 0)
            {
                Console.WriteLine($"\nA quantidade informada é inválida. Código precisa ser positivo");
                Console.WriteLine($"Não será realizada alteração no estoque.");
                Thread.Sleep(5000);
                return false;
            }
            if(quantidade > estoqueSavlo.Quantidade)
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine($"\n Quantidade solicitada não disponível no estoque. Quantidade Disponível: {estoqueSavlo.Quantidade}\n");
                Console.ResetColor();
                Thread.Sleep(5000);
                return false;
            }
            return true;
        }

        private static void MostrarCodigosVendas()
        {
            var contador = 1;
            foreach (var @enum in Enum.GetValues(typeof(TipoVenda)))
            {
                Console.WriteLine($" {contador} - {@enum}");
                contador++;
            }
        }


    }
}
