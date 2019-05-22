using Chamado.Models;
using Entity;
using FrameworkCAS;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class DAL_Parametro : Base_DAL
    {
        public List<Mod_Parametro> Consulta(List<string> pListParametro)
        {
            Mod_Consulta vMod_Consulta = new Mod_Consulta();
            vMod_Consulta.ListParametro = pListParametro;

            List<Mod_Parametro> vListaParametro = new List<Mod_Parametro>();
            RetornaDataSet vResult = ExecutaProcedure("<schema>.Pr_ConsultaParametro", vMod_Consulta);

            foreach (DataRow vLinha in vResult.DataSet.Tables[0].Rows)
            {
                vListaParametro.Add(Util.FormatarObjeto.LeDataRowERetornaObjetoPreenchido<Mod_Parametro>(vLinha));
            }

            return vListaParametro;
        }
    }
}