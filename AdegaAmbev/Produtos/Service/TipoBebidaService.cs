using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AdegaAmbev.Produtos.Entidades;

namespace AdegaAmbev.Produtos.Service {
    public class TipoBebidaService
    {
        private string Host { get; set; }

        public TipoBebidaService()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\Produto.json";
        }

        public void CadastrarTipoBebida(TipoBebida tipoBebida){
            using FileStream stream = File.OpenRead(Host);
            var tipoBebidaDb = JsonSerializer.DeserializeAsync<List<TipoBebida>>(stream).Result;
            stream.Close();
            var qtdTipoBebida = tipoBebidaDb.Count;
            tipoBebida.SetId(++qtdTipoBebida);
            tipoBebidaDb.Add(tipoBebida);

            File.WriteAllText(Host, JsonSerializer.Serialize(tipoBebidaDb));
        }

        public bool ExisteTipoBebida(string tipoBebida)
        {
            using FileStream stream = File.OpenRead(Host);
            var tipoBebidaDb = JsonSerializer.DeserializeAsync<List<TipoBebida>>(stream).Result;
            stream.Close();
            return tipoBebidaDb.Any(x => x.Nome == tipoBebida);
        }
    }
}