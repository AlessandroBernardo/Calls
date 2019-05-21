using Chamado.Models;
using Entity;
using FrameworkCAS;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class DAL_Anexo : Base_DAL
    {
        public Mod_Anexo Consulta(int pIdAnexo)
        {
            Mod_Consulta vMod_Consulta = new Mod_Consulta();
            vMod_Consulta.IdAnexo = pIdAnexo;

            List<Mod_Anexo> vListaAnexo = new List<Mod_Anexo>();
            RetornaDataSet vResult = ExecutaProcedure("<schema>.Pr_ConsultaAnexo", vMod_Consulta);

            foreach (DataRow vLinha in vResult.DataSet.Tables[0].Rows)
            {
                vListaAnexo.Add(Util.FormatarObjeto.LeDataRowERetornaObjetoPreenchido<Mod_Anexo>(vLinha));
            }

            return vListaAnexo.FirstOrDefault();
        }
    }
}