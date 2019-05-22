using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chamado.Models
{
    public class Enums
    {
        public enum TipoChamado
        {
            Erro,
            Dúvida,
            Outros
        }

        public enum Sistemas
        {
            Sicob,
            CapGiro,
            Outros
        }   
    }
}