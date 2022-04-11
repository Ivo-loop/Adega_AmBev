using AdegaAmbev.Comum;
using AdegaAmbev.Estoque.Entidades;
using System;
using System.Reflection;

namespace TestRoots
{
    public class EstoqueTest
    {
        public void Deve_Atualizar_Quantidade_Em_Estoque()
        {
            var nomeMetodo = MethodBase.GetCurrentMethod().Name;

            var quantidadeEstoque = 20;
            var atualizarQuantidade = 25;
            var estoque = new Estoque(1, quantidadeEstoque);

            estoque.AtualizarQuantidade(atualizarQuantidade);

            if (estoque.Quantidade == atualizarQuantidade)
            {
                MensagemSucesso(nomeMetodo);
            }
            else
            {
                MensagemErro(nomeMetodo, MensagemResultadoFinalNaoEsperado(estoque.Quantidade, atualizarQuantidade));
            }

            Console.ResetColor();
        }

        public void Deve_Subtrair_Quantidade_Em_Estoque()
        {
            var nomeMetodo = MethodBase.GetCurrentMethod().Name;

            var quantidadeEstoque = 20;
            var quantidadeSub = 18;
            var resultadoFinalEsperado = quantidadeEstoque - quantidadeSub;
            var estoque = new Estoque(1, quantidadeEstoque);

            estoque.SubtrairQuantidade(quantidadeSub);

            if (estoque.Quantidade == resultadoFinalEsperado)
            {
                MensagemSucesso(nomeMetodo);
            }
            else
            {
                MensagemErro(nomeMetodo, MensagemResultadoFinalNaoEsperado(estoque.Quantidade, resultadoFinalEsperado));
            }

            Console.ResetColor();
        }

        private string MensagemResultadoFinalNaoEsperado(object resultadoFinal, object resultadoEsperado)
        {
            return $"resultado final: {resultadoFinal}, resultado esperado: {resultadoEsperado}";
        }

        private void MensagemErro(string nomeMetodo, string mensagem)
        {
            CorLetraConsole.Vermelho();
            Console.WriteLine($"{nomeMetodo}: Falha, o resultado final não foi o esperado. ({mensagem})");
        }

        private void MensagemSucesso(string nomeMetodo)
        {
            CorLetraConsole.Verde();
            Console.WriteLine($"{nomeMetodo}: Sucesso.");
        }
    }
}
