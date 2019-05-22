using BLL;
using System.Web;
using System.Web.Mvc;
using Util;

namespace Chamado.Controllers
{
    public class _BaseController : Controller
    {
        protected virtual CustomPrincipal UsuarioLogado
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        public bool ValidaUsuario()
        {
            if (Session["UsuarioLogado"] == null)
                return false;

            System.Web.HttpContext.Current.User = (CustomPrincipal)Session["UsuarioLogado"];
            return true;
        }

        public BLL_Anexo BLL_Anexo = new BLL_Anexo();
        public BLL_Parametro BLL_Parametro = new BLL_Parametro();
    }
}