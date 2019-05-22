using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chamado.Models
{
    public class Mod_Parametro
    {
        public Int32 IdParametro { get; set; }
        public String NomeParametro { get; set; }
        public String ValorParametro { get; set; }
        public DateTime DtInclusao { get; set; }
        public String NmUsuarioInclusao { get; set; }
        public Boolean FlAtivo { get; set; }
    }
}