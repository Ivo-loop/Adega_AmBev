using AdegaAmbev.Comum;
using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Service;
using System;
using TestRoots.Common;

namespace TestRoots
{
    public class TesteProdutoServiceRoots
    {
        private readonly ProdutoService _produtoService = new ProdutoService();

        public TesteProdutoServiceRoots(ProdutoService produdoService)
        {
            _produtoService = produdoService;
        }

        public void Deve_retornar_mensagem_de_verdadeiro_ao_cadastrar_produto() 
        {
            limpar_banco();
            var mensagem = cadastrar_produto_no_banco();

            if (mensagem)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste passou o valor de retorno foi TRUE, Produto cadastrado com sucesso");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou o valor de retorno foi FALSO, ou seja, não foi possível cadastrar o Produto");
            }
        }

        public void Deve_retornar_mensagem_de_falso_ao_cadastrar_produto()
        {
            limpar_banco();
            var mensagem = cadastrar_produto_no_banco();

            if (!mensagem)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste passou o valor de retorno foi FALSO, ou seja, não foi possível cadastrar o Produto");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou o valor de retorno foi TRUE, Produto cadastrado com sucesso");
            }
        }

        public void Deve_retornar_mensagem_de_verdadeiro_ao_atualizar_produto()
        {
            limpar_banco();
            Produto produto = new Produto("Skol", "Cerveja", 3.48);

            var mensagem = _produtoService.AtualizarProduto(1, produto);

            if (mensagem)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste passou o valor de retorno foi TRUE, Produto atualizado com sucesso");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou o valor de retorno foi FALSO, ou seja, não foi possível atualizar o Produto");
            }
        }

        public void Deve_retornar_mensagem_de_falso_ao_atualizar_produto()
        {
            limpar_banco();
            Produto produto = new Produto("Skol", "", 3.48);

            var mensagem = _produtoService.AtualizarProduto(1, produto);

            if (!mensagem)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste passou o valor de retorno foi FALSO, ou seja, não foi possível atualizar o Produto");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou o valor de retorno foi TRUE, Produto atualizado com sucesso");
            }
        }

        public void Deve_retornar_mensagem_de_verdadeiro_ao_buscar_todos_produtos()
        {
            limpar_banco();
            var inserirProduto = cadastrar_produto_no_banco();
            var produtos = _produtoService.BuscarTodosOsProdutos();

            if (produtos.Count == 1 && inserirProduto)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Um produdo existe no banco \n");
                Console.WriteLine("Teste passou, TRUE, uma lista contendo o produto foi retornada");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou, FALSO, nenhuma lista de produtos foi retornada");
            }
        }

        public void Deve_retornar_mensagem_de_falso_ao_buscar_todos_produtos()
        {
            limpar_banco();
            var produtos = _produtoService.BuscarTodosOsProdutos();

            if (produtos.Count == 1)
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou, TRUE, uma lista contendo o produto foi retornada");
            }
            else
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste passou, FALSO, nenhuma lista de produtos foi retornada");
            }
        }

        public void Deve_retornar_mensagem_de_verdadeiro_ao_buscar_um_produto_por_id()
        {
            limpar_banco();
            var inserirProduto = cadastrar_produto_no_banco();
            var produtos = _produtoService.GetId(1);

            if (produtos.Id == 1 && inserirProduto)
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Um produdo existe no banco \n");
                Console.WriteLine("Teste passou, TRUE, um produto foi retornado");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou, FALSO, nenhum produto foi retornado");
            }
        }

        public void Deve_retornar_mensagem_de_falso_ao_buscar_um_produto_por_id()
        {
            limpar_banco();
            var produtos = _produtoService.GetId(1);

            if (produtos.Id == 1)
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou, TRUE, uma lista contendo o produto foi retornada");
            }
            else
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste passou, FALSO, nenhuma lista de produtos foi retornada");
            }
        }

        private bool cadastrar_produto_no_banco() 
        {
            Produto produto = new Produto("Skol", "Cerveja", 3.48);
            return _produtoService.CadastrarProduto(produto);
        }

        private void limpar_banco() 
        {
             new BancoUtils("Produto.json");
 
        }

    }
}
