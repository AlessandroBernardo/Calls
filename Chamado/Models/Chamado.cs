using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Chamado.Models
{
    public class Chamado
    {
        public int IdChamado { get; set; }        
        [Required(ErrorMessage = "Por favor, descreva o problema!")]
        public string Descricao { get; set; }        
        [Required(ErrorMessage="Por favor, nos diga o motivo!")]
        public string Motivo { get; set; }
        public string EstagioProj { get; set; }
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Por favor, informe qual sistema!")]
        public string Sistema { get; set; }
        public string NumChamado { get; set; }
        public string Status { get; set; }
        public string Tela { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Responsavel { get; set; }
        public virtual ICollection<Mod_Anexo> Anexos { get; set; }        
    }   
}




