﻿using AdegaAmbev.Comum;
using AdegaAmbev.Estoque.Repository;
using AdegaAmbev.Estoque.Service;
using System;
using System.Linq;
using TestRoots.Common;

namespace TestRoots
{
    internal class VendaServiceTest
    {
        public async void Deve_Realizar_Venda()
        {
            limpar_banco();
            await VendaService.RealizarVenda();

            var vendasRepository = new VendaRepository();

            if (vendasRepository.ObterTodos().Any())
            {
                CorLetraConsole.Verde();
                Console.WriteLine("Teste realizado com sucesso");
            }
            else
            {
                CorLetraConsole.Vermelho();
                Console.WriteLine("Teste falhou");
            }
        }

        private void limpar_banco()
        {
            new BancoUtils("Produto.json");

        }
    }
}
