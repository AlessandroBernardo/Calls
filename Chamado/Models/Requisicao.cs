using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chamado.Models
{
    public class Requisicao
    {
        public int IdRequisicao { get; set; }
        public string Motivo { get; set; }
        public string EstagioProjeto { get; set; }
        public string Sistema { get; set; }
        public string Descricao { get; set; }
    }
}