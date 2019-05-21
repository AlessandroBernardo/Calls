using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;

namespace Util
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }

    public partial class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string vDsApelido)
        {
            this.Identity = new GenericIdentity(vDsApelido);
            ListFuncionalidade = new List<string>();
        }

        public string DsLogin { get; set; }
        public string NomeCompleto { get; set; }
        public List<string> ListFuncionalidade { get; set; }

        public bool IsInRole(string vRole)
        {
            return Identity != null &&
                   Identity.IsAuthenticated &&
                   !string.IsNullOrWhiteSpace(vRole) &&
                   (ListFuncionalidade.Contains("AdmGen") || ListFuncionalidade.Contains(vRole));
        }
    }
}

//using System.Collections.Generic;
//using System.Security.Principal;
//using System.Web.Mvc;

//namespace Util
//{
//    public abstract class BaseViewPage : WebViewPage
//    {
//        public virtual new CustomPrincipal User
//        {
//            get { return base.User as CustomPrincipal; }
//        }
//    }

//    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
//    {
//        public virtual new CustomPrincipal User
//        {
//            get { return base.User as CustomPrincipal; }
//        }
//    }

//    public class CustomPrincipal : IPrincipal
//    {
//        public IIdentity Identity { get; private set; }

//        public CustomPrincipal(string vDsApelido)
//        {
//            this.Identity = new GenericIdentity(vDsApelido);
//        }

//        public string NomeCompleto { get; set; }
//        public string DsLogin { get; set; }
//        public List<string> ListFuncionalidade { get; set; }

//        public bool IsInRole(string vRole)
//        {
//            return Identity != null &&
//                   Identity.IsAuthenticated &&
//                   !string.IsNullOrWhiteSpace(vRole) &&
//                   (ListFuncionalidade.Contains("AdmGen") ||
//                    ListFuncionalidade.Contains("ConGen") && vRole.Substring(0, 3) == "Con" ||
//                    ListFuncionalidade.Contains(vRole));
//        }
//    }

//    public class CustomPrincipalSerializeModel
//    {
//        public string NomeCompleto { get; set; }
//        public string DsApelido { get; set; }
//        public string DsLogin { get; set; }
//        public List<string> ListFuncionalidade { get; set; }
//    }
//}