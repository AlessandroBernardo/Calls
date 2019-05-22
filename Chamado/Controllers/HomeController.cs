using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chamado.Models;
using Entity;
using Chamado.Properties;
using FrameworkCAS;
using Util;
using DAL;

namespace Chamado.Controllers
{
    public class HomeController : _BaseController
    {
        //
        // GET: /Home/Index
        public ActionResult Index(string au)
        {
            Session["UsuarioLogado"] = null;
            ViewBag.MsgErro = TempData["MsgErro"];
            return View("~/Views/Chamado/Index.cshtml");
        }
        

        //
        // GET: /Home/Login
        public ActionResult Login()
        {
            Session["UsuarioLogado"] = null;
            List<string> vListParamNome = new List<string>();
            vListParamNome.Add("Url Autenticação");

            return Redirect(BLL_Parametro.Consulta(vListParamNome).FirstOrDefault().ValorParametro);
        }

        public void AutenticarUsuarioCAS(string pCdFuncional)
        {
            CustomPrincipal vNewUser = new CustomPrincipal("");
            Tb_Usuario vUsuario = new Tb_Usuario();

            try
            {
                pCdFuncional = DLL_CAS.Autenticacao.FormatarMatricula(pCdFuncional);
                vUsuario = DLL_CAS.Autenticacao.MapearPerfilSQL(pCdFuncional, Settings.Default.SiglaSistema, Settings.Default.Ambiente);
                vNewUser = new CustomPrincipal(vUsuario.DsApelido);
                vNewUser.NomeCompleto = vUsuario.NomeUsuario;
                vNewUser.DsLogin = vUsuario.DsLogin;
                vNewUser.ListFuncionalidade = vUsuario.ListSistemas.SelectMany(x => x.List_Perfil).SelectMany(x => x.ListFuncionalidade).Select(y => y.SiglaFuncionalidade).Distinct().ToList();
            }
            catch (Exception ex)
            {
                TempData["MsgErro"] = ex.Message;
            }

            Session["UsuarioLogado"] = vNewUser;
            System.Web.HttpContext.Current.User = vNewUser;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}