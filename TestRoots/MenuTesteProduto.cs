using AdegaAmbev.Produtos.Service;
using System;

namespace TestRoots
{
    public static class MenuTesteProduto
    { 
        public static void IniciarMenuProduto()
        {
            var produtoService = new ProdutoService();
            TesteProdutoServiceRoots teste = new(produtoService);
            Console.WriteLine("Iniciando Testes de Produto\n");

            Console.WriteLine("Teste de Cadastrar de Produto\n");
            teste.Deve_retornar_mensagem_de_verdadeiro_ao_cadastrar_produto();
            teste.Deve_retornar_mensagem_de_falso_ao_cadastrar_produto();

            Console.WriteLine("Teste de Atualizar de Produto\n");
            teste.Deve_retornar_mensagem_de_verdadeiro_ao_atualizar_produto();
            teste.Deve_retornar_mensagem_de_falso_ao_atualizar_produto();

            Console.WriteLine("Teste de Buscar Todos os Produto\n");
            teste.Deve_retornar_mensagem_de_verdadeiro_ao_buscar_todos_produtos();
            teste.Deve_retornar_mensagem_de_falso_ao_buscar_todos_produtos();

            Console.WriteLine("Teste de Buscar Produto por ID\n");
            teste.Deve_retornar_mensagem_de_verdadeiro_ao_buscar_um_produto_por_id();
            teste.Deve_retornar_mensagem_de_falso_ao_buscar_um_produto_por_id();

            Console.WriteLine("Teste para Verificar se Produto Existe\n");
            teste.Deve_retornar_mensagem_de_verdadeiro_ao_verificar_se_existe_produto();
            teste.Deve_retornar_mensagem_de_falso_ao_verificar_se_existe_produto();

            Console.WriteLine("Teste de Buscar Produtos Por Filtro\n");
            teste.Deve_retornar_mensagem_de_verdadeiro_ao_buscar_um_produto_por_filtro();
            teste.Deve_retornar_mensagem_de_falso_ao_buscar_um_produto_por_filtro();
        }
    }
}
