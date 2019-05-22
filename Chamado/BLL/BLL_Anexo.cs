using Chamado.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class BLL_Anexo
    {
        public Mod_Anexo Consulta(int pIdAnexo)
        {
            DAL_Anexo DAL_Anexo = new DAL_Anexo();
            return DAL_Anexo.Consulta(pIdAnexo);
        }
    }
}