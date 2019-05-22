using Chamado.Properties;
using Entity;
using FrameworkCAS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace DAL
{
    public class Base_DAL
    {
        public RetornaDataSet ExecutaProcedure(string pProcedure, object pRegistro = null)
        {
            StringConexao();
            DAOSqlServerModeloRetorno vModeloRetorno = new DAOSqlServerModeloRetorno(VGFramework.StringConexao, short.Parse(VGFramework.StringConexao.Substring(VGFramework.StringConexao.LastIndexOf("=") + 1)));

            RetornaDataSet vRetorno = new RetornaDataSet();

            SqlCommand vSql = new SqlCommand();
            vSql.Connection = new SqlConnection(VGFramework.StringConexao);
            vSql.CommandType = CommandType.StoredProcedure;
            vSql.CommandText = pProcedure;
            vSql = AdicionaParametro(vSql, pRegistro);

            vRetorno = vModeloRetorno.ExecutaDataSet(vSql);

            if (vRetorno.TipoMensagem.ToString() == "Erro")
            {
                throw new Exception(vRetorno.Mensagem);
            }

            return vRetorno;
        }

        public SqlCommand AdicionaParametro(SqlCommand vSql, object pRegistro = null)
        {
            if (pRegistro != null)
            {
                foreach (PropertyInfo vPropriedade in pRegistro.GetType().GetProperties())
                {
                    var vValor = pRegistro.GetType().GetProperty(vPropriedade.Name).GetValue(pRegistro, null);
                    if (vValor != null)
                    {
                        //Nao mapear tipo objeto
                        if (pRegistro.GetType().GetProperty(vPropriedade.Name).PropertyType.FullName.IndexOf("Entity") < 0)
                        {
                            //Formatar List para xml
                            if (pRegistro.GetType().GetProperty(vPropriedade.Name).PropertyType.Name == "List`1")
                            {
                                var vStringwriter = new StringWriter();
                                var vSerializer = new XmlSerializer(vValor.GetType());
                                vSerializer.Serialize(vStringwriter, vValor);

                                vSql.Parameters.AddWithValue("@" + vPropriedade.Name, vStringwriter.ToString().Substring(vStringwriter.ToString().IndexOf("\r\n")));
                            }
                            else
                            {
                                vSql.Parameters.AddWithValue("@" + vPropriedade.Name, vValor);
                            }
                        }
                    }
                }
            }

            return vSql;
        }

        public static void StringConexao()
        {
            if (VGFramework.StringConexao == null)
            {
                Mod_ConsultaListarStringConexao vMod_ConsultaListarStringConexao = new Mod_ConsultaListarStringConexao();
                vMod_ConsultaListarStringConexao.DsAmbiente = Settings.Default.Ambiente;
                vMod_ConsultaListarStringConexao.SiglaSistema = Settings.Default.SiglaSistema;

                Tb_StringConexao vTb_StringConexao = DLL_CAS.Autenticacao.ConsultarStringConexao(Settings.Default.SiglaSistema, Settings.Default.Ambiente);

                VGFramework.StringConexao = " Data Source=" + vTb_StringConexao.NmServidor +
                    ";Initial Catalog=" + vTb_StringConexao.NmBase +
                    ";User Id=" + vTb_StringConexao.NmUsuario +
                    ";Password=" + SCV.Util.Descriptografar(vTb_StringConexao.DsSenha) +
                    ";Application Name=" + Assembly.GetExecutingAssembly().GetName().Name +
                    ";Connection Timeout=" + vTb_StringConexao.NuTimeOut;
            }
        }
    }
}









//using FrameworkCAS;
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace DAL
//{
//    public class BaseDAL
//    {
//        public static RetornaDataSet ExecutaProcedure(string pProcedure, object pRegistro = null)
//        {
//            RetornaDataSet vRetorno = new RetornaDataSet();

//            SqlCommand vSql = new SqlCommand();
//            vSql.CommandType = CommandType.StoredProcedure;
//            vSql.Connection = new SqlConnection(VGFramework.StringConexao);
//            vSql.CommandText = pProcedure;

//            if (pRegistro != null)
//            {
//                foreach (System.Reflection.PropertyInfo propriedade in pRegistro.GetType().GetProperties())
//                {
//                    var vValor = pRegistro.GetType().GetProperty(propriedade.Name).GetValue(pRegistro, null);
//                    if (vValor != null)
//                    {
//                        if (propriedade.Name == "DsSenhaSistema" || propriedade.Name == "DsSenha")
//                        {
//                            vSql.Parameters.AddWithValue("@" + propriedade.Name, SCV.Util.Criptografar(vValor.ToString()));
//                        }
//                        else
//                        {
//                            vSql.Parameters.AddWithValue("@" + propriedade.Name, vValor);
//                        }
//                    }
//                }
//            }

//            DAOSqlServerModeloRetorno vModeloRetorno = new DAOSqlServerModeloRetorno(VGFramework.StringConexao, short.Parse(VGFramework.StringConexao.Substring(VGFramework.StringConexao.LastIndexOf("=") + 1)));
//            vRetorno = vModeloRetorno.ExecutaDataSet(vSql);

//            if (vRetorno.TipoMensagem.ToString() == "Erro")
//            {
//                throw new Exception(vRetorno.Mensagem);
//            }

//            return vRetorno;
//        }
//    }
//}