using AdegaAmbev.Comum;
using AdegaAmbev.Produtos.Entidades;
using AdegaAmbev.Produtos.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TestRoots
{
    public class TipoBebidaServiceTeste
    {
        private readonly TipoBebidaService _tipoBebidaService;
        private string diretorio { get; set; }

        public TipoBebidaServiceTeste()
        {
            _tipoBebidaService = new TipoBebidaService();
            diretorio = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\TipoBebida.json";
        }

        public void testValidarCadastroTipoBebida()
        {
            var tipoBebidaModel = new TipoBebida()
            {
                Id = 1,
                Nome = "Bhrama"
            };

            _tipoBebidaService.CadastrarTipoBebida(tipoBebidaModel);

            using FileStream stream = File.OpenRead(diretorio);
            var tipoBebidaDb = JsonSerializer.DeserializeAsync<List<TipoBebida>>(stream).Result;
            stream.Close();
            tipoBebidaDb.ForEach(tipoBebida =>
            {
                if (tipoBebida.Id == tipoBebidaModel.Id && tipoBebida.Nome == tipoBebidaModel.Nome)
                { 
                    CorLetraConsole.Verde(); 
                    Console.WriteLine("Cadastro de tipo bebida realizado com sucesso"); 
                }
                else
                {
                    CorLetraConsole.Vermelho(); 
                    Console.WriteLine("Cadastro de tipo bebida não realizado");
                }
                Console.ResetColor();
            });

            File.WriteAllText(diretorio, "[]");
        }
        public void testValidarExisteTipoBebida()
        {
            var tipoBebida = "Bhrama";

            _tipoBebidaService.ExisteTipoBebida(tipoBebida);

            using FileStream stream = File.OpenRead(diretorio);
            stream.Close();
            if (!_tipoBebidaService.ExisteTipoBebida(tipoBebida))
            {
                CorLetraConsole.Vermelho(); 
                Console.WriteLine("Produto não possui cadastro");
            }
            else
            {
                CorLetraConsole.Verde(); 
                Console.WriteLine("Produto encontrado");
            }
            Console.ResetColor();
        }
    }
}