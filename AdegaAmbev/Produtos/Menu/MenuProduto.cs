using AdegaAmbev.Comum;
using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace AdegaAmbev.Produtos.Menu
{
    public static class MenuProduto
    {
        public static void IniciarMenuProduto()
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

            switch (Console.Read())
            {
                case '1':
                    Console.ReadLine();
                    Console.WriteLine("Insira tipo de bebida: ");
                    var nomeTipoBebida = Console.ReadLine();

                    var tipoBebida = new TipoBebida() { Nome = nomeTipoBebida };
                    tipoBebidaService.CadastrarTipoBebida(tipoBebida);

                    CorLetraConsole.Verde();
                    Console.WriteLine("Tipo Bebida Cadastrada");
                    CorLetraConsole.Preto();
                    break;
                case '2':
                    Console.Clear();
                    Console.ReadLine();
                    Console.WriteLine("Insira o nome do Produto: ");
                    var nomeProduto = Console.ReadLine();

                    Console.WriteLine("Insira o tipo de bebida: ");
                    var nomeTipoDeBebida = Console.ReadLine();

                    Console.WriteLine("Insira o valor do produto: ");
                    var valorProduto = double.Parse(Console.ReadLine());

                    var novoProduto = new Produto(nomeProduto, nomeTipoDeBebida, valorProduto);
                    var sucess = produtoService.CadastrarProduto(novoProduto);
                    if (sucess)
                    {
                        CorLetraConsole.Verde();
                        Console.WriteLine("Bebida Cadastrada! ja podemos experimentar chefe?");
                        CorLetraConsole.Preto();
                    }
                    break;

                case '3':
                    Console.ReadLine();
                    Console.WriteLine("Insira o ID do Produto: ");
                    var idProduto = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Insira o novo nome do Produto: ");
                    var nomeProdutoAtualizado = Console.ReadLine();

                    Console.WriteLine("Insira o novo valor do produto: ");
                    var valorProdutoAtualizado = double.Parse(Console.ReadLine());

                    Console.WriteLine("Insira o novo tipo de bebida: ");
                    var nomeTipodebebidaAtualizada = Console.ReadLine();

                    var produtoAtualizado = new Produto(nomeProdutoAtualizado, nomeTipodebebidaAtualizada, valorProdutoAtualizado);

                    produtoService.AtualizarProduto(idProduto, produtoAtualizado);
                    break;

                case '4':
                    var produtos = produtoService.BuscarTodosOsProdutos();
                    CorLetraConsole.Azul();
                    Console.WriteLine(string.Join("\n", produtos.Select(x=>JsonSerializer.Serialize(x)).ToList()));
                    CorLetraConsole.Preto();
                    Console.ReadLine();
                    break;

                case '5':
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Insira o nome do Produto: ");
                    var nome = Console.ReadLine();

                    Console.WriteLine("Insira o tipo de bebida: ");
                    var nometipoBebida = Console.ReadLine();
                    
                    var resultadoBusca = produtoService.BuscarProdutosPorFiltros(nome, nometipoBebida);
                    if(resultadoBusca.Count == 0)
                    {
                        CorLetraConsole.Vermelho();
                        Console.WriteLine("Não encontrou Resutado para sua busca");
                        CorLetraConsole.Preto();
                    }
                    CorLetraConsole.Azul();
                    Console.WriteLine(string.Join("\n", resultadoBusca.Select(x => JsonSerializer.Serialize(x)).ToList()));
                    CorLetraConsole.Preto();
                    break;

                case '0':
                    return;
                default:
                    Console.WriteLine("Opção inválida, tente novamente");
                    Console.ReadLine();
                    break;
            }
            IniciarMenuProduto();
        }
    }
}
