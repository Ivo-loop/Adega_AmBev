using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdegaAmbev.Produtos.Menu
{
    public class MenuProduto
    {
        public void IniciarMenuProduto()
        {
            var produtoService = new ProdutoService();
            TipoBebidaService tipoBebidaService = new TipoBebidaService();

            Console.WriteLine("Digite a opção desejada:\n");
            Console.WriteLine("1 - Cadastrar tipo de bebida");
            Console.WriteLine("2 - Cadastrar produto");
            Console.WriteLine("3 - Atualizar produto");
            Console.WriteLine("4 - Visualizar todos os produto");
            Console.WriteLine("5 - Pesquisar produtos");
            Console.WriteLine("0 - Sair\n");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Insira tipo de bebida: ");
                    var nomeTipoBebida = Console.ReadLine();
                    var tipoBebida = new TipoBebida() { Nome = nomeTipoBebida };
                    tipoBebidaService.CadastrarTipoBebida(tipoBebida);
                    break;
                case "2":
                    Console.Write("Insira o nome do Produto: ");
                    var nomeProduto = Console.ReadLine();

                    Console.Write("\n Insira o tipo de bebida: ");
                    var nomeTipoDeBebida = Console.ReadLine();

                    Console.Write("\n Insira o valor do produto: ");
                    var valorProduto = double.Parse(Console.ReadLine());

                    var novoProduto = new Produto(nomeProduto, nomeTipoDeBebida, valorProduto);
                    produtoService.CadastrarProduto(novoProduto);
                    break;

                case "3":
                    Console.Write("Insira o ID do Produto: ");
                    var idProduto = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Insira o nome do Produto: ");
                    var nomeProdutoAtualizado = Console.ReadLine();

                    Console.Write("\n Insira o valor do produto: ");
                    var valorProdutoAtualizado = double.Parse(Console.ReadLine());

                    Console.Write("Insira o tipo de bebida: ");
                    var nomeTipodebebidaAtualizada = Console.ReadLine();

                    var produto = produtoService.GetId(idProduto);
                    var produtoAtualizado = new Produto(nomeProdutoAtualizado, nomeTipodebebidaAtualizada, valorProdutoAtualizado);

                    produtoService.AtualizarProduto(idProduto, produtoAtualizado);
                    break;

                case "4":
                    var produtos = produtoService.BuscarTodosOsProdutos();
                    Console.WriteLine(JsonSerializer.Serialize(produtos));
                    break;

                case "5":
                    Console.Write("Insira o nome do Produto: ");
                    var nome = Console.ReadLine();
                    Console.Write("\n Insira o tipo de bebida: ");
                    var nometipoBebida = Console.ReadLine();
                    var resultadoBusca = produtoService.BuscarProdutosPorFiltros(nome, nometipoBebida);
                    Console.WriteLine(JsonSerializer.Serialize(resultadoBusca));
                    break;

                case "0":
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente");
                    break;
            }
        }
    }
}
