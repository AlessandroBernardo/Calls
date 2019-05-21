using DAL;
using Chamado.Properties;
using FrameworkCAS;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;

namespace Util
{
    public class Log : Base_DAL
    {
        public String DsEvento { get; set; }
        public DateTime? DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
        public String DsParametros { get; set; }
        public String DsResultado { get; set; }
        public Boolean? FlSucesso { get; set; }
        public String DsUsuarioInclusao { get; set; }
        public String DsSistema { get; set; }
        public String NuVersao { get; set; }
        public String DsAmbiente { get; set; }

        public Log(string pEvento)
        {
            DsEvento = pEvento;
            DtInicio = DateTime.Now;
            FlSucesso = true;
            DsResultado = "Sucesso.";
            DsUsuarioInclusao = (!HttpContext.Current.User.Identity.IsAuthenticated) ? "Não Logado" : (HttpContext.Current.User as CustomPrincipal).DsLogin;
            DsSistema = Assembly.GetExecutingAssembly().GetName().Name;
            NuVersao = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DsAmbiente = Settings.Default.Ambiente;
        }

        public void InsereLog()
        {
            SqlCommand comando = new SqlCommand("SenhaUserSis.Pr_InsLogEventos");

            try
            {
                Base_DAL.StringConexao();
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@DsEvento", DsEvento);
                comando.Parameters.AddWithValue("@DtInicio", DtInicio);
                comando.Parameters.AddWithValue("@DtFim", DateTime.Now);
                comando.Parameters.AddWithValue("@DsParametros", (string.IsNullOrEmpty(DsParametros) ? (object)DBNull.Value : DsParametros));
                comando.Parameters["@DsParametros"].Size = 8000;
                comando.Parameters.AddWithValue("@DsResultado", DsResultado);
                comando.Parameters["@DsResultado"].Size = 8000;
                comando.Parameters.AddWithValue("@FlSucesso", FlSucesso);
                comando.Parameters.AddWithValue("@NmUsuarioInclusao", DsUsuarioInclusao);
                comando.Parameters.AddWithValue("@NmSistema", DsSistema);
                comando.Parameters.AddWithValue("@NuVersao", NuVersao);
                comando.Parameters.AddWithValue("@DsAmbiente", DsAmbiente);

                StringConexao();
                DAOSqlServerModeloRetorno vModeloRetorno = new DAOSqlServerModeloRetorno(VGFramework.StringConexao, short.Parse(VGFramework.StringConexao.Substring(VGFramework.StringConexao.LastIndexOf("=") + 1)));
                vModeloRetorno.ExecutaComando(comando);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}