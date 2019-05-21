using Chamado.Models;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class BLL_Parametro
    {
        public List<Mod_Parametro> Consulta(List<string> pListParametro)
        {
            DAL_Parametro DAL_Parametro = new DAL_Parametro();
            return DAL_Parametro.Consulta(pListParametro);
        }
    }
}