using Chamado.Properties;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Chamado.DAL
{
    public class DALChamados 
    {
        public List<Tb_StringConexao> Consulta()
        {
            Mod_ConsultaListarStringConexao vModConsulta = new Mod_ConsultaListarStringConexao();

            Mod_RetornoListarStringConexao vRetorno = DLL_CAS.ServicoCAS.ListarStringConexao(vModConsulta, Settings.Default.Ambiente);

            if (vRetorno.Mensagem != null)
            {
                throw new Exception(vRetorno.Mensagem);
            }

            List<Tb_StringConexao> vListStringConexao = vRetorno.ListStringConexao.ToList();
            return vListStringConexao ?? new List<Tb_StringConexao>();
        }
    }
}