using System;
using System.Collections.Generic;
using AdegaAmbev.Produtos.Entidades;

namespace AdegaAmbev.Produtos.Repositorio {
    public class RepositorioTipoBebida
    {
        private readonly string Host {get; set;}

        public RepositorioTipoBebida()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Banco\TipoBebida.json";
        }
        public void CadastrarTipoBebida(){
        
        }
    }
}