using AdegaAmbev.Estoque.Repository;
using System;
using System.Threading;

namespace AdegaAmbev.Estoque.Service
{
    public static class EstoqueService
    {
        public static void InserirEstoque()
        {
            Console.Clear();
            var estoqueRepository = new EstoqueRepository();

            Console.WriteLine("Bem vindo ao Menu para inserir ESTOQUE\n");
            
            Console.Write("Digite o código do produto: ");
            var codigoProduto = Convert.ToInt32(Console.ReadLine());
            //Verificar se o produto existe no futuro.

            Console.Write("Digite a quantidade: ");
            var quantidade = Convert.ToInt32(Console.ReadLine());
            if (!EhQuantidadeValida(quantidade))
            {
                return;
            }

            var estoque = new Entidades.Estoque(codigoProduto, quantidade);
            estoqueRepository.Create(estoque);

        }

        private static bool EhQuantidadeValida(int codigo)
        {
            if (codigo <= 0)
            {
                Console.WriteLine($"\nA quantidade informada é inválida. Código precisa ser positivo");
                Console.WriteLine($"Não será realizada alteração no estoque.");
                Thread.Sleep(5000);
                return false;
            }
            return true;
        }
    }
}
