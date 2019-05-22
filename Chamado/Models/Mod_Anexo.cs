using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chamado.Models
{
    public class Mod_Anexo
    {
        public int IdAnexo { get; set; }
        public string NmArquivo { get; set; }
        public string TipoArquivo { get; set; }
        public byte[] Arquivo { get; set; }
        public DateTime DtInclusao { get; set; }
        public string NmUsuarioInclusao { get; set; }
        public virtual Chamado Chamado { get; set; }
        public int IdChamado { get; set; }
    }
}